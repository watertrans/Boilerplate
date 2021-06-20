using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class RefreshTokenRepositoryTest
    {
        [TestMethod]
        public void Create_�o�^���ɗ�O���������Ȃ�()
        {
            var now = TestEnvironment.DateTimeProvider.Now;
            var refreshToken = new RefreshToken
            {
                Token = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                ApplicationId = Guid.NewGuid(),
                PrincipalType = PrincipalType.Application.ToString(),
                PrincipalId = Guid.NewGuid(),
                Scopes = new List<string>(new string[] { Scopes.FullControl }),
                Status = RefreshTokenStatus.NORMAL,
                ExpiryTime = DateTime.MaxValue,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var refreshTokenRepository = new RefreshTokenRepository(TestEnvironment.DBSettings);
            refreshTokenRepository.Create(refreshToken);
        }

        [TestMethod]
        public void GetById_���݂���f�[�^���擾�ł���()
        {
            var refreshTokenRepository = new RefreshTokenRepository(TestEnvironment.DBSettings);
            var refreshToken = refreshTokenRepository.GetById("normal");
            Assert.IsNotNull(refreshToken);
        }

        [TestMethod]
        public void Update_�o�^�����f�[�^���X�V�ł���()
        {
            var refreshTokenRepository = new RefreshTokenRepository(TestEnvironment.DBSettings);
            var refreshToken = refreshTokenRepository.GetById("normal");
            refreshToken.UpdateTime = TestEnvironment.DateTimeProvider.Now;
            Assert.IsTrue(refreshTokenRepository.Update(refreshToken));
        }

        [TestMethod]
        public void Delete_�o�^�����f�[�^���폜�ł���()
        {
            var now = TestEnvironment.DateTimeProvider.Now;
            var refreshToken = new RefreshToken
            {
                Token = new string('Y', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                ApplicationId = Guid.NewGuid(),
                PrincipalType = PrincipalType.Application.ToString(),
                PrincipalId = Guid.NewGuid(),
                Scopes = new List<string>(new string[] { Scopes.FullControl }),
                Status = RefreshTokenStatus.SUSPENDED,
                ExpiryTime = DateTime.MaxValue,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var refreshTokenRepository = new RefreshTokenRepository(TestEnvironment.DBSettings);
            refreshTokenRepository.Create(refreshToken);
            Assert.IsTrue(refreshTokenRepository.Delete(refreshToken.Token));
        }
    }
}
