using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Triton.Core;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class CustomerAnalysisRepository : ICustomerAnalysis
    {
        private readonly IConfiguration _config;

        public CustomerAnalysisRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<long> PostAsync(List<CustomerAnalysis> customerAnalysis)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
                return connection.Insert(customerAnalysis);
            }
            catch
            {
                return 0;
            }
        }
    }
}
