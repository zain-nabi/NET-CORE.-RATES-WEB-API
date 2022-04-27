using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Service.Model.CRM.Custom;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface IWorkFlow
    {
        Task<List<WorkFlowStages>> GetWorkFlowStagesForNewIncreases(int WorkFlowID, string DatabaseName);
        Task<List<CustomerModel>> GetAllCustomers(int WorkFlowStageID);
    }
}
