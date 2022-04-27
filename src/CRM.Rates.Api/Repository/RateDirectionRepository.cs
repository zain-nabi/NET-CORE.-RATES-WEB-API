using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class RateDirectionRepository : IRateDirection
    {
        private readonly IConfiguration _config;

        public RateDirectionRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<RateDirections>> GetRateDirectionAsync()
        {
            const string sql = "SELECT * FROM RateDirections";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<RateDirections>(sql).ToList();
        }
    }
}
