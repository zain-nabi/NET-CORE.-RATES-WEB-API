using System.Threading.Tasks;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.TritonGroup.Custom;

namespace CRM.Rates.WebApi.Interface
{
    public interface IUploadDocument
    {
       Task<UploadDocumentModel> GetDocumentCategorysAsync();
       Task<bool> InsertIncreaseDocumentsAsync(IncreaseDocuments increaseDocuments);
    }
}
