using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Model.TritonSecurity.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface IBranch
    {
        Task<List<Branches>> GetRepTargetBranches();
        Task<Branches> GetBranchByID(int BranchID);
    }
}
