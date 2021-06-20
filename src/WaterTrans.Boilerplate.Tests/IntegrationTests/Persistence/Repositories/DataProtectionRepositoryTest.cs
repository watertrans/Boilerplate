using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class DataProtectionRepositoryTest
    {
        [TestMethod]
        public void GetAllElements_��O���������Ȃ�()
        {
            var authorizationCodeRepository = new DataProtectionRepository(TestEnvironment.DBSettings, TestEnvironment.DateTimeProvider);
            authorizationCodeRepository.GetAllElements();
        }

        [TestMethod]
        public void StoreElement_�o�^���ɗ�O���������Ȃ�()
        {
            var element = XElement.Parse("<root><item></item></root>");

            var authorizationCodeRepository = new DataProtectionRepository(TestEnvironment.DBSettings, TestEnvironment.DateTimeProvider);
            authorizationCodeRepository.StoreElement(element, "elementKey1");
        }

        [TestMethod]
        public void StoreElement_�o�^�������e���擾�ł���()
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
