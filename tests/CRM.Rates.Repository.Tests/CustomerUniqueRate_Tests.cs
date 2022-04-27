using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CRM.Rates.WebApi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRM.Rates.Repository.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CustomerUniqueRate_Tests
    {
        private static IConfiguration GetConfig()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        //[TestMethod]
        //public async Task PostCustomerUniqueRateAsync_Success()
        //{
        //    var customerAnalysis = new CustomerUniqueRateRepository(GetConfig());

        //    using (new TransactionScope())
        //    {
        //        var accountCode = "FLO104";
        //        var xml = "";
        //        var result = await customerAnalysis.PostCustomerUniqueRateAsync(accountCode, xml);

        //        Assert.IsNotNull(result);
        //        Assert.AreEqual(result, 1);
        //        Assert.IsInstanceOfType(result, typeof(long));
        //    }
        //}
    }
}
