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
    public class AccountRepositoryTest
    {
        [TestMethod]
        public void Create_登録時に例外が発生しない()
        {
            var now = DateUtil.Now;
            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Roles = new List<string>(new string[] { Roles.Contributor }),
                Status = AccountStatus.NORMAL,
                LastLoginTime = null,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var accountRepository = new AccountRepository(TestEnvironment.DBSettings);
            accountRepository.Create(account);
        }

        [TestMethod]
        public void GetById_存在するデータを取得できる()
        {
            var accountRepository = new AccountRepository(TestEnvironment.DBSettings);
            var account = accountRepository.GetById(Guid.Parse("00000000-B001-0000-0000-000000000000"));
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void Update_登録したデータを更新できる()
        {
            var accountRepository = new AccountRepository(TestEnvironment.DBSettings);
            var account = accountRepository.GetById(Guid.Parse("00000000-B001-0000-0000-000000000000"));
            account.LastLoginTime = DateUtil.Now;
            Assert.IsTrue(accountRepository.Update(account));
        }

        [TestMethod]
        public void Delete_登録したデータを削除できる()
        {
            var now = DateUtil.Now;
            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Roles = new List<string>(new string[] { Roles.Contributor }),
                Status = AccountStatus.NORMAL,
                LastLoginTime = null,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var accountRepository = new AccountRepository(TestEnvironment.DBSettings);
            accountRepository.Create(account);
            Assert.IsTrue(accountRepository.Delete(account.AccountId));
        }
    }
}
