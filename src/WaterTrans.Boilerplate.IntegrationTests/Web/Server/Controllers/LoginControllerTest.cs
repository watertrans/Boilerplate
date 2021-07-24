using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using WaterTrans.Boilerplate.IntegrationTests;

namespace WaterTrans.Boilerplate.Web.Server.Controllers.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests.Web.Server")]
    public class LoginControllerTest
    {
        private readonly HttpClient _httpclient;

        public LoginControllerTest()
        {
            _httpclient = TestEnvironment.WebServerFactory.CreateClient();
        }

        [TestMethod]
        public void Index_200応答が返る()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Login");
            var response = _httpclient.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
            var responseBody = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
