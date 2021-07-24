using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Infrastructure.Cryptography;
using WaterTrans.Boilerplate.Infrastructure.OS;
using WaterTrans.Boilerplate.IntegrationTests;
using WaterTrans.Boilerplate.Persistence.Repositories;

namespace WaterTrans.Boilerplate.Domain.Services.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class AccountServiceTest
    {
        private AccountService _accountService;
        private PasswordHashProvider _passwordHashProvider;
        private DateTimeProvider _dateTimeProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            _dateTimeProvider = new DateTimeProvider();
            _passwordHashProvider = new PasswordHashProvider();
            var accountRepository = new AccountRepository(TestEnvironment.DBSettings);
            _accountService = new AccountService(
                accountRepository,
                _passwordHashProvider,
                _dateTimeProvider);
        }

        [TestMethod]
        public void GetAccount_存在しないキーでnullを返す()
        {
            Assert.AreEqual(null, _accountService.GetAccount(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetAccount_存在するキーでインスタンスを返す()
        {
            Assert.AreNotEqual(null, _accountService.GetAccount(Guid.Parse("00000000-B001-0000-0000-000000000000")));
        }

        [TestMethod]
        public void GetAccountByLoginId_存在しないログインIDでnullを返す()
        {
            Assert.AreEqual(null, _accountService.GetAccountByLoginId(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void GetAccountByLoginId_存在するログインIDでインスタンスを返す()
        {
            Assert.AreNotEqual(null, _accountService.GetAccountByLoginId("admin"));
        }

        [TestMethod]
        public void UpdateLastLoginTime_LastLoginTimeが更新される()
        {
            var accountId = Guid.Parse("00000000-B001-0000-0000-000000000000");
            var account1 = _accountService.GetAccount(accountId);
            var expected = account1.LastLoginTime;
            _accountService.UpdateLastLoginTime(accountId);
            var account2 = _accountService.GetAccount(accountId);
            Assert.AreNotEqual(expected, account2.LastLoginTime);
        }

        [TestMethod]
        public void VerifyPassword_一致()
        {
            var accountId = Guid.Parse("00000000-B001-0000-0000-000000000000");
            var account = _accountService.GetAccount(accountId);
            Assert.IsTrue(_accountService.VerifyPassword("admin-secret", account));
        }
    }
}
