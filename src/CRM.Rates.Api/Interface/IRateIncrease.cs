using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Model.CRM.StoredProcs;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface IRateIncrease
    {
        Task<bool> InsertRateIncreaseAsync(RateIncreases model);
        Task<bool> UpdateRateIncreaseAsync(RateIncreases model);
        Task<List<proc_RateIncrease_Select>> GetRateIncreasesPerCycle(int RateCycleID);
        Task<RateIncreases> CheckIfRateIncreaseExist(string RateIncreasePeriod);
        Task<List<proc_RateIncrease_Select>> GetRateIncreases();
        Task<proc_RateIncrease_Select> GetRateIncreaseByID(int RateIncreaseID);
        Task<RateIncreases> GetTop1RateIncrease();
    }
}
