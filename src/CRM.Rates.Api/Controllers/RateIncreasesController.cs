using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.StoredProcs;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateIncreasesController : Controller
    {
        private readonly IRateIncrease _rateIncrease;

        public RateIncreasesController(IRateIncrease rateIncrease)
        {
            _rateIncrease = rateIncrease;
        }

        [HttpPost("InsertRateIncreaseAsync/{Model}")]
        [SwaggerOperation(Summary = "InsertRateIncreaseAsync - Inserts a rate increase record into the system", Description = "Returns true if successful ")]
        public async Task<ActionResult<bool>> InsertRateIncreaseAsync(RateIncreases model)
        {
            return await _rateIncrease.InsertRateIncreaseAsync(model);
        }

        [HttpPut("UpdateRateIncreaseAsync/{Model}")]
        [SwaggerOperation(Summary = "UpdateRateIncreaseAsync - Updates a rate increase record into the system", Description = "Returns true if successful ")]
        public async Task<ActionResult<bool>> UpdateRateIncreaseAsync(RateIncreases model)
        {
            return await _rateIncrease.UpdateRateIncreaseAsync(model);
        }

        [Route("GetRateIncreasesPerCycle/{RateCycleID}")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetRateIncreasesPerCycle - Returns a rate increase per cycle", Description = "Returns a rate increase if successful ")]
        public async Task<ActionResult<List<proc_RateIncrease_Select>>> GetRateIncreasesPerCycle(int RateCycleID)
        {
            return await _rateIncrease.GetRateIncreasesPerCycle(RateCycleID);
        }

        [Route("CheckIfRateIncreaseExist/{RateIncreasePeriod}")]
        [HttpGet]
        [SwaggerOperation(Summary = "CheckIfRateIncreaseExist - Checks for duplicate rate increase", Description = "Returns true/false if successful ")]
        public async Task<ActionResult<RateIncreases>> CheckIfRateIncreaseExist(string RateIncreasePeriod)
        {
            return await _rateIncrease.CheckIfRateIncreaseExist(RateIncreasePeriod);
        }

        [Route("GetRateIncreases")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetRateIncreases - Returns  rate increase per cycle", Description = "Returns  rate increase if successful ")]
        public async Task<ActionResult<List<proc_RateIncrease_Select>>> GetRateIncreases()
        {
            return await _rateIncrease.GetRateIncreases();
        }

        [Route("GetRateIncreaseByID/{RateIncreaseID}")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetRateIncreaseByID - Returns  rate increase by id", Description = "Returns  rate increase if successful ")]
        public async Task<ActionResult<proc_RateIncrease_Select>> GetRateIncreaseByID(int RateIncreaseID)
        {
            return await _rateIncrease.GetRateIncreaseByID(RateIncreaseID);
        }

        [Route("GetTop1RateIncrease")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetTop1RateIncrease - Returns the last rate increase entry", Description = "Returns RateIncreases model if successful ")]
        public async Task<ActionResult<RateIncreases>> GetTop1RateIncrease()
        {
            return await _rateIncrease.GetTop1RateIncrease();
        }
    }
}
