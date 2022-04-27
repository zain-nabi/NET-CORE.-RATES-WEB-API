using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.StoredProcs;

namespace CRM.Rates.WebApi.Repository
{
    public class RateRepository : IRate
    {
        private readonly IConfiguration _config;

        public RateRepository(IConfiguration configuration)
        {
            _config = configuration;
        }


        public async Task<List<proc_Rates_Matrix_Select>> GetRatesMatrixAsync(int customerId, int rateYear, bool? isCrossBorder)
        {
            const string sql = "proc_Rates_Matrix_Select";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<proc_Rates_Matrix_Select>(sql, new { CustomerID = customerId, RateYear = rateYear, isCrossBorder }, commandType: CommandType.StoredProcedure).ToList();
        }

        public async Task<int> GetActiveRateYear()
        {
            const string sql = "SELECT MAX(RateYear) AS RateYear FROM Rates WHERE Active = 1";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<int>(sql).FirstOrDefault();
        }

        public async Task<int> GetLatestRateYear()
        {
            const string sql = "SELECT MAX(RateYear) AS RateYear FROM Rates";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<int>(sql).FirstOrDefault();
        }
    }
}
