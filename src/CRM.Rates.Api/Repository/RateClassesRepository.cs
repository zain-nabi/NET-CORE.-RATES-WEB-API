using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class RateClassesRepository : IRateClasses
    {
        private readonly IConfiguration _config;
        public RateClassesRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<RateClasses>> GetAsync()
        {
            const string sql = "SELECT RateClassID, RTRIM([Description]) AS [Description], RateCycleID, Active, canQuote, canAdjust FROM RateClasses";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<RateClasses>(sql).ToList();
        }
    }
}
