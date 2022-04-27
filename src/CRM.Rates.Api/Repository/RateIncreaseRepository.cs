using CRM.Rates.WebApi.Interface;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Triton.Core;
using Triton.Model.CRM.StoredProcs;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class RateIncreaseRepository : IRateIncrease
    {
        private readonly IConfiguration _config;

        public RateIncreaseRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<bool> InsertRateIncreaseAsync(RateIncreases model)
        {
            try
            {
                // Scope transaction
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                // Set the connection
                await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
                _ = await connection.InsertAsync(model);

                // Save the record
                scope.Complete();

                // Return success
                return true;
            }
            catch //(Exception e)
            {
                // Log error
                return false;
            }
        }

        public async Task<bool> UpdateRateIncreaseAsync(RateIncreases model)
        {
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return await connection.UpdateAsync(model);
        }

        public async Task<List<proc_RateIncrease_Select>> GetRateIncreasesPerCycle(int RateCycleID)
        {
            const string sql = "proc_RateIncreasePerCycle_Select";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<proc_RateIncrease_Select>(sql, new { RateCycleID }, commandType: CommandType.StoredProcedure).ToList();

        }

        public async Task<RateIncreases> CheckIfRateIncreaseExist(string RateIncreasePeriod)
        {
            const string sql = "proc_RateIncrease_CheckIfRateIncreaseExist";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<RateIncreases>(sql, new { RateIncreasePeriod }, commandType: CommandType.StoredProcedure).FirstOrDefault();

        }

        public async Task<List<proc_RateIncrease_Select>> GetRateIncreases()
        {
            const string sql = "proc_RateIncrease_Select";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<proc_RateIncrease_Select>(sql, commandType: CommandType.StoredProcedure).ToList();

        }

        public async Task<proc_RateIncrease_Select> GetRateIncreaseByID(int RateIncreaseID)
        {
            const string sql = "proc_RateIncreaseByID_Select";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<proc_RateIncrease_Select>(sql,new { RateIncreaseID}, commandType: CommandType.StoredProcedure).FirstOrDefault();

        }

        public async Task<RateIncreases> GetTop1RateIncrease()
        {
            const string sql = "SELECT TOP 1 * FROM RateIncreases ORDER BY RateIncreaseID DESC";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<RateIncreases>(sql).FirstOrDefault();
        }
    }
}
