using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface IRateDirection
    {
        Task<List<RateDirections>> GetRateDirectionAsync();
    }
}
