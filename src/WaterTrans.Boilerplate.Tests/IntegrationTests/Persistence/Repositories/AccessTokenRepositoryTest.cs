using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class AccessTokenRepositoryTest
    {
        [TestMethod]
        public void Create_登録時に例外が発生しない()
        {
            var now = DateUtil.Now;
            var accessToken = new AccessToken
            {
                Token = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                ApplicationId = Guid.NewGuid(),
                PrincipalType = PrincipalType.Application.ToString(),
                PrincipalId = Guid.NewGuid(),
                Scopes = new List<string>(new string[] { Scopes.FullControl }),
                Status = AccessTokenStatus.NORMAL,
                ExpiryTime = DateTime.MaxValue,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var accessTokenRepository = new AccessTokenRepository(TestEnvironment.DBSettings);
            accessTokenRepository.Create(accessToken);
        }

        [TestMethod]
        public void GetById_存在するデータを取得できる()
        {
            var accessTokenRepository = new AccessTokenRepository(TestEnvironment.DBSettings);
            var accessToken = accessTokenRepository.GetById("normal");
            Assert.IsNotNull(accessToken);
        }

        [TestMethod]
        public void Update_登録したデータを更新できる()
        {
            var accessTokenRepository = new AccessTokenRepository(TestEnvironment.DBSettings);
            var accessToken = accessTokenRepository.GetById("normal");
            accessToken.UpdateTime = DateUtil.Now;
            Assert.IsTrue(accessTokenRepository.Update(accessToken));
        }

        [TestMethod]
        public void Delete_登録したデータを削除できる()
        {
            var now = DateUtil.Now;
            var accessToken = new AccessToken
            {
                Token = new string('Y', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                ApplicationId = Guid.NewGuid(),
                PrincipalType = PrincipalType.Application.ToString(),
                PrincipalId = Guid.NewGuid(),
                Scopes = new List<string>(new string[] { Scopes.FullControl }),
                Status = AccessTokenStatus.SUSPENDED,
                ExpiryTime = DateTime.MaxValue,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var accessTokenRepository = new AccessTokenRepository(TestEnvironment.DBSettings);
            accessTokenRepository.Create(accessToken);
            Assert.IsTrue(accessTokenRepository.Delete(accessToken.Token));
        }
    }
}
