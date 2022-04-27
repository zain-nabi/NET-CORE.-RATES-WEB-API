using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface IRateCycle
    {
        Task<List<RateCycles>> GetAllRateCycles();
    }
}
