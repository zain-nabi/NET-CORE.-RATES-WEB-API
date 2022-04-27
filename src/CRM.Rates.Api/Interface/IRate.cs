using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Model.CRM.StoredProcs;

namespace CRM.Rates.WebApi.Interface
{
    public interface IRate
    {
        Task<List<proc_Rates_Matrix_Select>> GetRatesMatrixAsync(int customerId, int rateYear, bool? isCrossBorder);
        Task<int> GetActiveRateYear();

        Task<int> GetLatestRateYear();
    }
}
