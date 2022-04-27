using System.Collections.Generic;
using System.Threading.Tasks;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.CRM.Custom;

namespace CRM.Rates.WebApi.Interface
{
    public interface IIncreaseDocument
    {
        Task<List<IncreaseDocumentModel>> GetIncreaseDocumentAsync(int customerAnalysisId);
        Task<IncreaseDocumentModel> GetIncreaseDocumentByDocumentID(int documentId);

        Task<IncreaseDocuments> GetIncreaseDocumentByAnalysisID(int customerAnalysisId);
    }
}
