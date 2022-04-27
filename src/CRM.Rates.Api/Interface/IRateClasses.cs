using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface IRateClasses
    {
        Task<List<RateClasses>> GetAsync();
    }
}
