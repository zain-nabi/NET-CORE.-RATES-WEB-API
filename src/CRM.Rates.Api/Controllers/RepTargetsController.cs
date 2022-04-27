using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.Tables;
using Triton.Model.LeaveManagement.Tables;
using Triton.Service.Model.CRM.Custom;
using Triton.Service.Model.CRM.StoredProcs;
using Triton.Service.Model.CRM.Tables;
using Triton.Service.Model.TritonSecurity.Custom;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepTargetsController : ControllerBase
    {
        private readonly IRepTarget _repTarget;

        public RepTargetsController(IRepTarget repTarget)
        {
            _repTarget = repTarget;
        }

        #region New Business

        [Route("GetCalenderYear")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetCalenderYear - Returns calender years", Description = "Returns calender years if successful ")]
        public async Task<ActionResult<List<Dates>>> GetCalenderYear()
        {
            return await _repTarget.GetCalenderYear();
        }

        [Route("GetRepTargetsNewBizGrid/{FinancialYear}/{UserID}/{TargetType}")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetRepTargetsNewBizGrid - Returns monthly matrix report for jul - jun", Description = "Returns matrix report")]
        public async Task<ActionResult<List<RepTargetsModel>>> GetRepTargetsNewBizGrid(int FinancialYear, int UserID, int TargetType)
        {
            return await _repTarget.GetRepTargetsNewBizGrid(FinancialYear, UserID, TargetType);
        }

        [Route("GetFinancialvsSaleTargetsNewBizGrid")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetFinancialvsSaleTargetsNewBizGrid - Returns sales vs financial targets", Description = "Returns matrix report")]
        public async Task<ActionResult<List<proc_FinancialvsSaleTargets>>> GetFinancialvsSaleTargetsNewBizGrid()
        {
            return await _repTarget.GetFinancialvsSaleTargetsNewBizGrid();
        }

        [Route("GetIndividualRepTarget/{RepCode}/{FinancialYear}/{TargetType}/{EmployeeID}")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetIndividualRepTarget - Returns individual rep target with images", Description = "Returns rep targets")]
        public async Task<ActionResult<RepTargetsModel>> GetIndividualRepTarget(string RepCode, int FinancialYear, int TargetType, int EmployeeID)
        {
            return await _repTarget.IndividualRepTarget(RepCode, FinancialYear, TargetType, EmployeeID);
        }

        [Route("GetFinancialYear")]
        [HttpGet]
        [SwaggerOperation(Summary = "GetFinancialYear - Returns financial year", Description = "Returns financial year")]
        public async Task<ActionResult<int>> GetFinancialYear(DateTime Date)
        {
            return await _repTarget.GetFinancialYear(Date);
        }

        #endregion
    }
}
