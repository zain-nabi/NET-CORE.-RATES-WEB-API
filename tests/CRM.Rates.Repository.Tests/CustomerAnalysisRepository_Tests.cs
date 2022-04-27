using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Transactions;
using CRM.Rates.WebApi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triton.Model.CRM.Tables;

namespace CRM.Rates.Repository.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CustomerAnalysisRepository_Tests
    {
        private static IConfiguration GetConfig()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static List<CustomerAnalysis> GetCustomerAnalysisListSuccessObject()
        {
            var customerAnalysisList = new List<CustomerAnalysis>
            {
                new CustomerAnalysis
                {
                    CustomerID = 123,
                    IncreaseDate = DateTime.Now.Date,
                    ProcessCompleted = true,
                    Ref = "Test123",
                    RateIncreaseID = 1,
                    HasOpsNote = true,
                   // IsManual =false,
                    ExcludeFromReview = false,
                    DeliveryDate = DateTime.Now.Date,
                    EffectiveDate = DateTime.Now.Date
                }
            };
            return customerAnalysisList;
        }

        private static List<CustomerAnalysis> GetCustomerAnalysisListFailureObject()
        {
            var customerAnalysisList = new List<CustomerAnalysis>
            {
                new CustomerAnalysis
                {
                    CustomerID = 123,
                    IncreaseDate = DateTime.Now.Date,
                    ProcessCompleted = true,
                    Ref = "Test123",
                    RateIncreaseID = 1,
                    HasOpsNote = true,
                    //IsManual = 0,
                    ExcludeFromReview = false,
                    EffectiveDate = DateTime.Now.Date
                }
            };
            return customerAnalysisList;
        }

        [TestMethod]
        public async Task PostAsync_Success()
        {
            var customerAnalysis = new CustomerAnalysisRepository(GetConfig());

            using (new TransactionScope())
            {
                var result = await customerAnalysis.PostAsync(GetCustomerAnalysisListSuccessObject());

                Assert.IsNotNull(result);
                Assert.AreEqual(result, 1);
                Assert.IsInstanceOfType(result, typeof(long));
            }
        }

        [TestMethod]
        public async Task PostAsync_Failed()
        {
            var customerAnalysis = new CustomerAnalysisRepository(GetConfig());

            using (new TransactionScope())
            {
                var result = await customerAnalysis.PostAsync(GetCustomerAnalysisListFailureObject());

                Assert.IsNotNull(result);
                Assert.AreEqual(result, 1);
                Assert.IsInstanceOfType(result, typeof(long));
            }
        }
    }
}
