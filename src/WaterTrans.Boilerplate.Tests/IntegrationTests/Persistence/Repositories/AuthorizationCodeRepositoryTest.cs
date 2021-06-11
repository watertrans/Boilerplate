using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class AuthorizationCodeRepositoryTest
    {
        [TestMethod]
        public void Create_�o�^���ɗ�O���������Ȃ�()
        {
            var now = DateUtil.Now;
            var authorizationCode = new AuthorizationCode
            {
                Code = new string('X', 100),
                ApplicationId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Status = AuthorizationCodeStatus.NORMAL,
                ExpiryTime = DateTimeOffset.MaxValue,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            authorizationCodeRepository.Create(authorizationCode);
        }

        [TestMethod]
        public void GetById_���݂���f�[�^���擾�ł���()
        {
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            var authorizationCode = authorizationCodeRepository.GetById("normal");
            Assert.IsNotNull(authorizationCode);
        }

        [TestMethod]
        public void Update_�o�^�����f�[�^���X�V�ł���()
        {
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            var authorizationCode = authorizationCodeRepository.GetById("normal");
            authorizationCode.UpdateTime = DateUtil.Now;
            Assert.IsTrue(authorizationCodeRepository.Update(authorizationCode));
        }

        [TestMethod]
        public void Delete_�o�^�����f�[�^���폜�ł���()
        {
            var now = DateUtil.Now;
            var authorizationCode = new AuthorizationCode
            {
                Code = new string('Y', 100),
                ApplicationId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Status = AuthorizationCodeStatus.USED,
                ExpiryTime = DateTimeOffset.MaxValue,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            authorizationCodeRepository.Create(authorizationCode);
            Assert.IsTrue(authorizationCodeRepository.Delete(authorizationCode.Code));
        }
    }
}
