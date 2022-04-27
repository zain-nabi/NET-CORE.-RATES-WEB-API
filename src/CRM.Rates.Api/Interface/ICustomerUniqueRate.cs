using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Model.CRM.StoredProcs;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Interface
{
    public interface ICustomerUniqueRate
    {
        Task<List<CustomerUniqueRates>> GetCustomerUniqueRateAsync(int customerId);

        Task<List<CustomerUniqueRates>> GetCustomerUniqueRateActiveAsync(int customerId);

        Task<long> PostCustomerUniqueRateAsync(string accountCode, string xml);

        Task<List<proc_Rates_Outlying_SlidingScale_Select>> GetOutlyingSlidingRates(int customerId);
    }
}
