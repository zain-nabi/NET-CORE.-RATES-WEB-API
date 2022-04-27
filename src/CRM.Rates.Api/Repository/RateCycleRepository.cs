using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class RateCycleRepository : IRateCycle
    {
        private readonly IConfiguration _config;

        public RateCycleRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<RateCycles>> GetAllRateCycles()
        {
            string sql = @"SELECT * FROM [CRM].[dbo].[RateCycles]";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<RateCycles>(sql, "").ToList();
        }
    }
}
