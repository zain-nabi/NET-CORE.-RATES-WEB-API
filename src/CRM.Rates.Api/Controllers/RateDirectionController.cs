using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateDirectionController : ControllerBase
    {
        private readonly IRateDirection _rate;

        public RateDirectionController(IRateDirection rate)
        {
            _rate = rate;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "GetRateDirectionAsync - Gets the rate direction Single/Bi-Directional", Description = "Returns List<RateDirections>")]
        public async Task<ActionResult<List<RateDirections>>> GetAsync()
        {
            return await _rate.GetRateDirectionAsync();
        }
    }
}
