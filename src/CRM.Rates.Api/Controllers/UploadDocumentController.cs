using System.Threading.Tasks;
using CRM.Rates.WebApi.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.TritonGroup.Custom;

namespace Triton.WorkFlow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadDocumentController : ControllerBase
    {
        private readonly IUploadDocument _uploadDocument;
        private readonly IRepTarget _uploadTarget;

        public UploadDocumentController(IUploadDocument uploadDocument, IRepTarget uploadTarget)
        {
            _uploadDocument = uploadDocument;
            _uploadTarget = uploadTarget;
        }

        [HttpGet("GetDocumentCategorysAsync")]
        [SwaggerOperation(Summary = "Get List of GetDocumentCategorysAsync ", Description = "Get List of GetDocumentCategorysAsync ")]
        public async Task<UploadDocumentModel> GetDocumentCategorysAsync()
        {
           return await _uploadDocument.GetDocumentCategorysAsync();
        }

        [HttpPost("InsertIncreaseDocumentsAsync")]
        [SwaggerOperation(Summary = "InsertIncreaseDocumentsAsync - Inserts a rate increase record into the system", Description = "Returns true if successful ")]
        public async Task<ActionResult<bool>> InsertIncreaseDocumentsAsync(IncreaseDocuments increaseDocuments)
        {
           return await _uploadDocument.InsertIncreaseDocumentsAsync(increaseDocuments);
        }

        [HttpGet("UploadReptarget")]
        [SwaggerOperation(Summary = "UploadReptarget - Inserts a rep Target into the system", Description = "Returns true if successful ")]
        public async Task<ActionResult<bool>> UploadReptarget(int targetType, int UserID, string FileName)
        {
            return await _uploadTarget.UploadReptarget(targetType, UserID, FileName);
        }
    }
}
