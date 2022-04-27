using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.TritonSecurity.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class BranchRepository : IBranch
    {
        private readonly IConfiguration _config;

        public BranchRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<Branches>> GetRepTargetBranches()
        {
            const string sql = @"SELECT * 
                                    FROM TritonSecurity.dbo.branches B 
                                    WHERE B.CompanyID = 1 AND B.Active = 1
                                    ORDER BY B.BranchFullName";

            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<Branches>(sql).ToList();
        }

        public async Task<Branches> GetBranchByID(int BranchID)
        {
            const string sql = @"SELECT * 
                                    FROM TritonSecurity.dbo.branches B 
                                    WHERE B.BranchID = @BranchID";

            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<Branches>(sql, new { BranchID = BranchID }).FirstOrDefault();
        }
    }
}
