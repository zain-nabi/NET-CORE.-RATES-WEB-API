using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Model.CRM.Tables;
using Triton.Model.LeaveManagement.Tables;
using Triton.Service.Model.CRM.Custom;
using Triton.Service.Model.CRM.StoredProcs;
using Triton.Service.Model.CRM.Tables;
using Triton.Service.Model.TritonSecurity.Custom;

namespace CRM.Rates.WebApi.Interface
{
    public interface IRepTarget
    {
        Task<List<Dates>> GetCalenderYear();
        Task<List<RepTargetsModel>> GetRepTargetsNewBizGrid(int FinancialYear, int UserID, int TargetType);
        Task<List<proc_FinancialvsSaleTargets>> GetFinancialvsSaleTargetsNewBizGrid();
        Task<bool> UploadReptarget(int targetType, int UserID, string FileName);
        Task<RepTargetsModel> IndividualRepTarget(string RepCode, int FinancialYear, int TargetType, int EmployeeID);
        Task<int> GetFinancialYear(DateTime Date);
    }
}
