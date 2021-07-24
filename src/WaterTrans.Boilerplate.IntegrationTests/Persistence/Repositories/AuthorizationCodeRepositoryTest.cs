using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.IntegrationTests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class AuthorizationCodeRepositoryTest
    {
        [TestMethod]
        public void Create_登録時に例外が発生しない()
        {
            var now = TestEnvironment.DateTimeProvider.Now;
            var authorizationCode = new AuthorizationCode
            {
                Code = new string('X', 100),
                ApplicationId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Status = AuthorizationCodeStatus.NORMAL,
                ExpiryTime = DateTime.MaxValue,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            authorizationCodeRepository.Create(authorizationCode);
        }

        [TestMethod]
        public void GetById_存在するデータを取得できる()
        {
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            var authorizationCode = authorizationCodeRepository.GetById("normal");
            Assert.IsNotNull(authorizationCode);
        }

        [TestMethod]
        public void Update_登録したデータを更新できる()
        {
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            var authorizationCode = authorizationCodeRepository.GetById("normal");
            authorizationCode.UpdateTime = TestEnvironment.DateTimeProvider.Now;
            Assert.IsTrue(authorizationCodeRepository.Update(authorizationCode));
        }

        [TestMethod]
        public void Delete_登録したデータを削除できる()
        {
            var now = TestEnvironment.DateTimeProvider.Now;
            var authorizationCode = new AuthorizationCode
            {
                Code = new string('Y', 100),
                ApplicationId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Status = AuthorizationCodeStatus.USED,
                ExpiryTime = DateTime.MaxValue,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            authorizationCodeRepository.Create(authorizationCode);
            Assert.IsTrue(authorizationCodeRepository.Delete(authorizationCode.Code));
        }
    }
}
