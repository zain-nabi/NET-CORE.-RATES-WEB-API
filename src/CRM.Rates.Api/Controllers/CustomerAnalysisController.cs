using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.CRM.Custom;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAnalysisController : ControllerBase
    {
        private readonly ICustomerAnalysis _customerAnalysis;
        private readonly IWorkFlow _workFlow;

        public CustomerAnalysisController(ICustomerAnalysis customerAnalysis, IWorkFlow workFlow)
        {
            _customerAnalysis = customerAnalysis;
            _workFlow = workFlow;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "PostAsync - Posts CustomerAnalysis and RateIncrease", Description = "Returns long")]
        public async Task<ActionResult<long>> PostAsync(List<CustomerAnalysis> customerAnalysis)
        {
            
            try
            {
                var t = await _customerAnalysis.PostAsync(customerAnalysis);
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("Customers/{WorkFlowStageID}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Users - Returns customer information", Description = "Returns customer information if successful ")]
        public async Task<ActionResult<List<CustomerModel>>> Customers(int WorkFlowStageID)
        {
            return await _workFlow.GetAllCustomers(WorkFlowStageID);
        }
    }
}
