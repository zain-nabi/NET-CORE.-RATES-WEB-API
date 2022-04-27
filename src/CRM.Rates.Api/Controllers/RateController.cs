using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.StoredProcs;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRate _rate;

        public RateController(IRate rate)
        {
            _rate = rate;
        }

        [HttpGet("GetRatesMatrixAsync/{customerId:int}/{rateYear:int}/{isCrossBorder}")]
        [SwaggerOperation(Summary = "GetRatesMatrixAsync - Get a rates for the specific customer", Description = "Returns List<proc_Rates_Matrix_Select>")]
        public async Task<ActionResult<List<proc_Rates_Matrix_Select>>> GetRatesMatrixAsync(int customerId, int rateYear, bool isCrossBorder = false)
        {
            return await _rate.GetRatesMatrixAsync(customerId, rateYear, isCrossBorder);
        }

        [HttpGet("GetRateYear")]
        [SwaggerOperation(Summary = "GetActiveRateYear - Get the latest active RateYear", Description = "Returns int")]
        public async Task<ActionResult<int>> GetActiveRateYear()
        {
            return await _rate.GetActiveRateYear();
        }

        [HttpGet("GetLatestRateYear")]
        [SwaggerOperation(Summary = "GetLatestRateYear - Get the latest RateYear even if its inactive", Description = "Returns int")]
        public async Task<ActionResult<int>> GetLatestRateYear()
        {
            return await _rate.GetLatestRateYear();
        }
    }
}
