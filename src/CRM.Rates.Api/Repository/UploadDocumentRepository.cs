using CRM.Rates.WebApi.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.TritonGroup.Custom;

namespace CRM.Rates.WebApi.Repository
{
    public class UploadDocumentRepository : IUploadDocument
    {
        private readonly IConfiguration _config;
        public UploadDocumentRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        
        public async Task<UploadDocumentModel> GetDocumentCategorysAsync()
        {
           await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
           string sql = $"EXEC dbo.proc_DocumentCategorys_Select";

           var uploadDocumentModel = new UploadDocumentModel();

           using (var multi = connection.QueryMultiple(sql))
            {
                uploadDocumentModel.DocumentCategorys = multi.Read<DocumentCategorys>().ToList();
                uploadDocumentModel.IncreaseDocuments = new IncreaseDocuments { };

           };
           return uploadDocumentModel;
        }

        public async Task<bool> InsertIncreaseDocumentsAsync(IncreaseDocuments increaseDocuments)
        {
            try
            {
               await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
               var result = connection.Query<bool>("[CRM].[dbo].[proc_IncreaseDocuments_Insert]",
               new
                {
                    increaseDocuments.AnalysisID,
                    increaseDocuments.ImgName,
                    increaseDocuments.ImgData,
                    increaseDocuments.ImgContentType,
                    increaseDocuments.ImgLength,
                    increaseDocuments.DateUploaded,
                    increaseDocuments.DocumentCategoryID,
                    increaseDocuments.EdocsDocID,
                    increaseDocuments.EdocsEnvelopeID
                },
               commandType: CommandType.StoredProcedure).FirstOrDefault();

               return true;
            }
            catch// (Exception e)
            {
                // Log error
                return false;
            }
        }
    }
}
