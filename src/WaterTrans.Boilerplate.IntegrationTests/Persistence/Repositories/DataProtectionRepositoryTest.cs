using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using WaterTrans.Boilerplate.IntegrationTests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class DataProtectionRepositoryTest
    {
        [TestMethod]
        public void GetAllElements_例外が発生しない()
        {
            var authorizationCodeRepository = new DataProtectionRepository(TestEnvironment.DBSettings, TestEnvironment.DateTimeProvider);
            authorizationCodeRepository.GetAllElements();
        }

        [TestMethod]
        public void StoreElement_登録時に例外が発生しない()
        {
            var element = XElement.Parse("<root><item></item></root>");

            var authorizationCodeRepository = new DataProtectionRepository(TestEnvironment.DBSettings, TestEnvironment.DateTimeProvider);
            authorizationCodeRepository.StoreElement(element, "elementKey1");
        }

        [TestMethod]
        public void StoreElement_登録した内容を取得できる()
        {
            var element = XElement.Parse("<hoge><item></item></hoge>");

            var authorizationCodeRepository = new DataProtectionRepository(TestEnvironment.DBSettings, TestEnvironment.DateTimeProvider);
            authorizationCodeRepository.StoreElement(element, "elementKey2");
            foreach (var item in authorizationCodeRepository.GetAllElements())
            {
                if (item.Name.LocalName == "hoge")
                {
                    return;
                }
            }

            Assert.Fail();
        }
    }
}
