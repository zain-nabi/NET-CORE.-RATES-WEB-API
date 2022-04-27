using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.Tables;
using Triton.Model.TritonSecurity.Tables;
using Triton.Service.Model.CRM.Custom;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Repository
{
    public class WorkFlowRepository : IWorkFlow
    {
        private readonly IConfiguration _config;

        public WorkFlowRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<WorkFlowStages>> GetWorkFlowStagesForNewIncreases(int WorkFlowID, string DatabaseName)
        {
            string sql = @"proc_WorkFlowStages_Select";

            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));
            return connection.Query<WorkFlowStages>(sql, new { DatabaseName, WorkFlowID  }, commandType: CommandType.StoredProcedure).ToList();
        }

        public async Task<List<CustomerModel>> GetAllCustomers(int WorkFlowStageID)
        {
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            string sql = @"EXEC proc_Customers_Branches_RateClasses_CustomerStatus_Select @WorkFlowStageID";

            var CustomerModel = new List<CustomerModel>();

            var data = connection.Query<Customers, Branches, RateClasses, CustomerStatuss, List<CustomerModel>>(
                 sql, (Customers, Branches, RateClasses, CustomerStatuss) =>
                 {
                     var model = new CustomerModel
                     {
                         Customers = Customers,
                         Branches = Branches,
                         RateClasses = RateClasses,
                         CustomerStatuss = CustomerStatuss
                     };

                     CustomerModel.Add(model);
                     return CustomerModel;
                 },
                 new { WorkFlowStageID },
                 splitOn: "CustomerID, BranchID, RateClassID, CustomerStatusID").FirstOrDefault();

            return data == null ? new List<CustomerModel>() : data;
        }
    }
}
