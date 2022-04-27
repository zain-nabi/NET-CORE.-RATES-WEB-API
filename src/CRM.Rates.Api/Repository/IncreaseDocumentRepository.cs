using CRM.Rates.WebApi.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triton.Core;
using Triton.Model.CRM.Tables;
using Triton.Service.Model.CRM.Custom;

namespace CRM.Rates.WebApi.Repository
{
    public class IncreaseDocumentRepository : IIncreaseDocument
    {
        private readonly IConfiguration _config;
        public IncreaseDocumentRepository(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<IncreaseDocumentModel>> GetIncreaseDocumentAsync(int customerAnalysisId)
        {
            const string sql = "proc_IncreaseDocuments_Select @CustomerAnalysisID ";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<IncreaseDocumentModel>(sql, new { CustomerAnalysisID = customerAnalysisId }).ToList();
        }

        public async Task<IncreaseDocumentModel> GetIncreaseDocumentByDocumentID(int documentId)
        {
            const string sql = "proc_IncreaseDocument_ByDocumentID @DocumentID ";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<IncreaseDocumentModel>(sql, new { DocumentID = documentId }).FirstOrDefault();
        }

        public async Task<IncreaseDocuments> GetIncreaseDocumentByAnalysisID(int customerAnalysisId)
        {
            const string sql = "SELECT * FROM IncreaseDocuments WHERE AnalysisID = @customerAnalysisId";
            await using var connection = DBConnection.GetOpenConnection(_config.GetConnectionString(StringHelpers.Database.Crm));

            return connection.Query<IncreaseDocuments>(sql, new { customerAnalysisId }).FirstOrDefault();
        }
    }
}
