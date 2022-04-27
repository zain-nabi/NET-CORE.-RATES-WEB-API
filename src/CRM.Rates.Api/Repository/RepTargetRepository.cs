using CRM.Rates.WebApi.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.Tables;
using Triton.Model.LeaveManagement.Tables;
using Triton.Model.TritonSecurity.Tables;
using Triton.Service.Model.CRM.Custom;
using Triton.Service.Model.CRM.StoredProcs;
using Triton.Service.Model.CRM.Tables;
using Triton.Service.Model.TritonSecurity.Custom;

namespace CRM.Rates.WebApi.Repository
{
    #region New Business
    public class RepTargetRepository : IRepTarget
    {
        private readonly IConfiguration _config;

        public RepTargetRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<Dates>> GetCalenderYear()
        {
            const string sql = @"SELECT DISTINCT Calendar_Year
                                    FROM Dates
                                 WHERE Calendar_Year > 2014
                                 AND Calendar_Year <= YEAR(DATEADD(YEAR, 1, GETDATE()))
                                 ORDER BY Calendar_Year DESC";

            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<Dates>(sql).ToList();

        }

        public async Task<List<RepTargetsModel>> GetRepTargetsNewBizGrid(int FinancialYear, int UserID, int TargetType)
        {
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            var query = @"EXEC proc_SalesTarget_Select @FinancialYear, @UserID, @TargetType";


            var customerModel = new List<RepTargetsModel>();

            var data = connection.Query<proc_RepTargetsNewBizGrid, Uploader, List<RepTargetsModel>>(
                 query, (NewBusiness, Uploader) =>
                 {
                     var model = new RepTargetsModel
                     {
                         NewBusinessTargets = NewBusiness,
                         Uploader = Uploader
                     };

                     customerModel.Add(model);
                     return customerModel;
                 },
                 new { FinancialYear, UserID, TargetType },
                 splitOn: "UploadID", commandTimeout: 120).FirstOrDefault();

            try
            {
                foreach (var item in data.ToList())
                {
                    if (item.Uploader != null)
                    {
                        if (item.Uploader.ContentType == "image/jpeg" || item.Uploader.ContentType == "image/png" || item.Uploader.ContentType == "image/pjpeg")
                        {
                            if (item.Uploader.Thumbnail == null)
                            {
                                using Image image = Image.Load(item.Uploader.FileData);

                                if (!(item.Uploader.Length > 10000)) return data;
                                var width = image.Width / 10;
                                var height = image.Height / 10;
                                image.Mutate(x => x.Resize(width, height, KnownResamplers.Lanczos3));

                                await using var memoryStream = new MemoryStream();
                                await image.SaveAsync(memoryStream, JpegFormat.Instance);
                                item.Uploader.FileData = item.Uploader.Thumbnail ?? memoryStream.ToArray();

                                await using var connectionLM = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.LeaveManagement));
                                var uploaderSql = "SELECT * FROM LeaveManagement.dbo.Uploader WHERE UploadID = @UploadID";
                                var uploader = connectionLM.Query<Uploader>(uploaderSql, new { item.Uploader.UploadID }).FirstOrDefault();
                                uploader.Thumbnail = memoryStream.ToArray();
                                connectionLM.Update(uploader);

                            }
                        }
                        else
                        {
                            item.Uploader.FileData = null;
                        }
                    }
                }
            }
            catch
            {
            }

            return data == null ? new List<RepTargetsModel>() : data;
        }

        public async Task<List<proc_FinancialvsSaleTargets>> GetFinancialvsSaleTargetsNewBizGrid()
        {
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            const string sql = "proc_FinancialvsSaleTargets";
            return connection.Query<proc_FinancialvsSaleTargets>(sql, commandType: CommandType.StoredProcedure).ToList();

        }

        public async Task<bool> UploadReptarget(int targetType, int UserID, string FileName)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
                string sql = $"EXEC proc_RepTargets_InsertTarget @targetType, @UserID, @FileName";
                var _ = connection.Query<bool>(sql, new { targetType, UserID, FileName }).FirstOrDefault();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<RepTargetsModel> IndividualRepTarget(string RepCode, int FinancialYear, int TargetType, int EmployeeID)
        {

            var RepTargetsModel = new RepTargetsModel();

            var sql = @"
                            SELECT * FROM LeaveManagement.dbo.Uploader WHERE EmployeeID = @EmployeeID
                            EXEC proc_RepTargets_ByRep_Select @FinancialYear, @RepCode, @TargetType";

            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            using (var multi = connection.QueryMultiple(sql, new { FinancialYear = FinancialYear, RepCode = RepCode, TargetType = TargetType, EmployeeID = EmployeeID }))
            {
                RepTargetsModel.Uploader = multi.Read<Uploader>().Single();
                RepTargetsModel.NewBusinessTargetsList = multi.Read<proc_RepTargetsNewBizGrid>().ToList();
            }

            try
            {
                if (RepTargetsModel.Uploader != null)
                {
                    if (RepTargetsModel.Uploader.ContentType == "image/jpeg" || RepTargetsModel.Uploader.ContentType == "image/png" || RepTargetsModel.Uploader.ContentType == "image/pjpeg")
                    {
                        using Image image = Image.Load(RepTargetsModel.Uploader.FileData);

                        if (!(RepTargetsModel.Uploader.Length > 10000)) return RepTargetsModel;
                        var width = image.Width / 40;
                        var height = image.Height / 40;
                        image.Mutate(x => x.Resize(width, height, KnownResamplers.Lanczos3));

                        await using var memoryStream = new MemoryStream();
                        await image.SaveAsync(memoryStream, JpegFormat.Instance);
                        RepTargetsModel.Uploader.FileData = memoryStream.ToArray();
                    }
                    else
                    {
                        RepTargetsModel.Uploader.FileData = null;
                    }
                }

            }
            catch
            {
            }

            return RepTargetsModel;
        }

        public async Task<int> GetFinancialYear(DateTime Date)
        {
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            string sql = $"SELECT CRM.dbo.fn_GetFinancialYear(@Date)";
            return connection.Query<int>(sql, new { Date = Date }).FirstOrDefault();
        }

    }
    #endregion
}