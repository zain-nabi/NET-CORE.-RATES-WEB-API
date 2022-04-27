using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.StoredProcs;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class CustomerUniqueRateRepository : ICustomerUniqueRate
    {
        private readonly IConfiguration _config;

        public CustomerUniqueRateRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<CustomerUniqueRates>> GetCustomerUniqueRateActiveAsync(int customerId)
        {
            const string sql = "SELECT * FROM CustomerUniqueRates WHERE DateEffectiveEnd IS NULL AND CustomerID = @CustomerID";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<CustomerUniqueRates>(sql, new {CustomerID = customerId}).ToList();
        }

        public async Task<List<CustomerUniqueRates>> GetCustomerUniqueRateAsync(int customerId)
        {
            const string sql = "SELECT * FROM CustomerUniqueRates WHERE CustomerID = @CustomerID";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<CustomerUniqueRates>(sql, new {CustomerID = customerId}).ToList();
        }

        public async Task<long> PostCustomerUniqueRateAsync(string accountCode, string xml)
        {
            const string sql = "proc_CustomerUniqueRates_Insert";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<long>(sql, new { accountCode, xml }).FirstOrDefault();
        }

        public async Task<List<proc_Rates_Outlying_SlidingScale_Select>> GetOutlyingSlidingRates(int customerId)
        {
            const string sql = "proc_Rates_Outlying_SlidingScale_Select";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<proc_Rates_Outlying_SlidingScale_Select>(sql, new { CustomerID = customerId }, commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
