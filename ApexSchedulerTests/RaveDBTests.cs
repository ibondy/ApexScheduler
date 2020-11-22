using ApexScheduler.RavenDB;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;

namespace ApexSchedulerTests
{
    [TestClass]
    public class RaveDBTests
    {
        [TestMethod]
        public async Task GetStatusTest()
        {
            var config = new ApexScheduler.ApexConfigs();
            config.RavenDBConfig.Url = new[] { "http://127.0.0.1:8080" };
            config.RavenDBConfig.DatabaseName = "ApexClient";
            var client = new RavenDBClient(null, config);
            var result = await client.GetStatus("Apex1");

            Assert.IsTrue(result.ApexId == "Apex1");
        }
    }
}