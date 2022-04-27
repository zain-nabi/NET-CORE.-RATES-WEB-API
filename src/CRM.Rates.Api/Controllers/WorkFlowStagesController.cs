using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Service.Model.CRM.Custom;
using Triton.Service.Model.CRM.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowStagesController : ControllerBase
    {
        private readonly IWorkFlow _workFlow;

        public WorkFlowStagesController(IWorkFlow workFlow)
        {
            _workFlow = workFlow;
        }

        [Route("WorkFlowStagesForNewIncreases/{WorkFlowID}/{DatabaseName}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Work Flow Stages - Returns a list of workflow stages for new rate increase", Description = "Returns work flow stages if successful ")]
        public async Task<ActionResult<List<WorkFlowStages>>> WorkFlowStagesForNewIncreases(int WorkFlowID, string DatabaseName)
        {
            return await _workFlow.GetWorkFlowStagesForNewIncreases(WorkFlowID, DatabaseName);
        }
    }
}
