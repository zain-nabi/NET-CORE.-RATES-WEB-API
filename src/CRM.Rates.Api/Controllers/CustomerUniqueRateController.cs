using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.StoredProcs;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerUniqueRateController : ControllerBase
    {
        private readonly ICustomerUniqueRate _rate;

        public CustomerUniqueRateController(ICustomerUniqueRate rate)
        {
            _rate = rate;
        }

        [HttpGet("GetCustomerUniqueRateActiveAsync")]
        [SwaggerOperation(Summary = "GetCustomerUniqueRateActiveAsync - Get the active unique rates for the specific customer", Description = "Returns List<CustomerUniqueRates>")]
        public async Task<ActionResult<List<CustomerUniqueRates>>> GetCustomerUniqueRateActiveAsync(int customerId)
        {
            return await _rate.GetCustomerUniqueRateActiveAsync(customerId);
        }

        [HttpGet("GetCustomerUniqueRateAsync")]
        [SwaggerOperation(Summary = "GetCustomerUniqueRateAsync - Get the unique rates for the specific customer", Description = "Returns List<CustomerUniqueRates>")]
        public async Task<ActionResult<List<CustomerUniqueRates>>> GetCustomerUniqueRateAsync(int customerId)
        {
            return await _rate.GetCustomerUniqueRateAsync(customerId);
        }

        [HttpGet("GetOutlyingSlidingRates/{customerId:int}")]
        [SwaggerOperation(Summary = "GetOutlyingSlidingRates - Get the outlying/sliding scale rates for the specific customer", Description = "Returns List<proc_Rates_Outlying_SlidingScale_Select>")]
        public async Task<ActionResult<List<proc_Rates_Outlying_SlidingScale_Select>>> GetOutlyingSlidingRates(int customerId)
        {
            return await _rate.GetOutlyingSlidingRates(customerId);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "PostAsync - Insert xml into CustomerUniqueRates / Bands", Description = "Returns long")]
        public async Task<ActionResult<long>> PostAsync(string accountCode, string xml)
        {
            return await _rate.PostCustomerUniqueRateAsync(accountCode, xml);
        }
    }
}
