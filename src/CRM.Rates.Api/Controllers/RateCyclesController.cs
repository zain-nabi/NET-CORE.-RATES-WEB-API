using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCyclesController : Controller
    {
        private readonly IRateCycle _rateCycle;

        public RateCyclesController(IRateCycle rateCycle)
        {
            _rateCycle = rateCycle;
        }

        [Route("RateCycles")]
        [HttpGet]
        [SwaggerOperation(Summary = "Rate Cycle - Returns a list of rate cycles", Description = "Returns rate cycles if successful ")]
        public async Task<ActionResult<List<RateCycles>>> RateCycles()
        {
            return await _rateCycle.GetAllRateCycles();
        }
    }
}
