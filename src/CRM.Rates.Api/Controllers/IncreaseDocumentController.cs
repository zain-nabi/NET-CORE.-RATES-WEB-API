using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.CRM.Custom;

namespace CRM.Rates.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncreaseDocumentController : ControllerBase
    {
        private readonly IIncreaseDocument _increaseDocument;
        public IncreaseDocumentController(IIncreaseDocument increaseDocument)
        {
            _increaseDocument = increaseDocument;
        }

        [Route("GetIncreaseDocumentByIdAsync")]
        [HttpGet]
        [SwaggerOperation(Summary = "IncreaseDocument - Returns a list of IncreaseDocument", Description = "Returns IncreaseDocument if successful ")]
        public async Task<ActionResult<List<IncreaseDocumentModel>>> GetIncreaseDocumentByIdAsync(int customerAnalysisId)
        {
            return await _increaseDocument.GetIncreaseDocumentAsync(customerAnalysisId);
        }

        [Route("GetIncreaseDocumentByDocumentID")]
        [HttpGet]
        [SwaggerOperation(Summary = "IncreaseDocument - Returns IncreaseDocument", Description = "Returns IncreaseDocument if successful ")]
        public async Task<ActionResult<IncreaseDocumentModel>> GetIncreaseDocumentByDocumentID(int documentId)
        {
            return await _increaseDocument.GetIncreaseDocumentByDocumentID(documentId);
        }

        [Route("GetIncreaseDocumentByAnalysisID")]
        [HttpGet]
        [SwaggerOperation(Summary = "IncreaseDocument - Returns a an IncreaseDocument", Description = "Returns IncreaseDocument")]
        public async Task<ActionResult<IncreaseDocuments>> GetIncreaseDocumentByAnalysisID(int customerAnalysisId)
        {
            return await _increaseDocument.GetIncreaseDocumentByAnalysisID(customerAnalysisId);
        }
    }
}
