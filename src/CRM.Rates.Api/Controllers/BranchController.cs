using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.TritonSecurity.Tables;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranch _branch;

        public BranchController(IBranch branch)
        {
            _branch = branch;
        }

        [Route("GetRepTargetBranches")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetRepTargetBranches - Returns rep brancges", Description = "Returns branches if successful ")]
        public async Task<ActionResult<List<Branches>>> GetRepTargetBranches()
        {
            return await _branch.GetRepTargetBranches();
        }

        [Route("GetBranchByID/{BranchID}")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetBranchByID - Returns specific branch", Description = "Returns branch if successful ")]
        public async Task<ActionResult<Branches>> GetBranchByID(int BranchID)
        {
            return await _branch.GetBranchByID(BranchID);
        }
    }
}
