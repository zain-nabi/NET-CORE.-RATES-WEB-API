using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateClassesController : ControllerBase
    {
         private readonly IRateClasses _rateClass;

        public RateClassesController(IRateClasses rateClass)
        {
            _rateClass = rateClass;
        }

        [HttpGet("GetAsync")]
        [SwaggerOperation(Summary = "GetAsync - Get all the active RateClasses", Description = "Returns List<RateClasses>")]
        public async Task<ActionResult<List<RateClasses>>> GetAsync()
        {
            return await _rateClass.GetAsync();
        }
    }
}
