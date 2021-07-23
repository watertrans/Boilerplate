using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Application.Settings;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Infrastructure.Cryptography;
using WaterTrans.Boilerplate.Infrastructure.OS;
using WaterTrans.Boilerplate.Persistence.Repositories;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Domain.Services.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class AuthorizeServiceTest
    {
        private AuthorizeService _authorizeService;
        private AppSettings _appSettings;
        private DateTimeProvider _dateTimeProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            _dateTimeProvider = new DateTimeProvider();
            _appSettings = new AppSettings
            {
                AccessTokenExpiresIn = 3600,
                AuthorizationCodeExpiresIn = 300,
                RefreshTokenExpiresIn = 43200
            };
            var accessTokenRepository = new AccessTokenRepository(TestEnvironment.DBSettings);
            var refrestTokenRepository = new RefreshTokenRepository(TestEnvironment.DBSettings);
            var authorizationCodeRepository = new AuthorizationCodeRepository(TestEnvironment.DBSettings);
            var applicationRepository = new ApplicationRepository(TestEnvironment.DBSettings);
            var accountRepository = new AccountRepository(TestEnvironment.DBSettings);
            var accountService = new AccountService(
                accountRepository,
                new PasswordHashProvider(),
                _dateTimeProvider);

            _authorizeService = new AuthorizeService(
                _appSettings,
                accessTokenRepository,
                refrestTokenRepository,
                authorizationCodeRepository,
                applicationRepository,
                accountService,
                _dateTimeProvider
                );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetApplication_null値は例外発生()
        {
            _authorizeService.GetApplication(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetApplication_空文字は例外発生()
        {
            _authorizeService.GetApplication(string.Empty);
        }

        [TestMethod]
        public void GetApplication_存在しないクライアントIDはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetApplication(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void GetApplication_停止しているアプリケーションを取得できる()
        {
            Assert.AreEqual(false, _authorizeService.GetApplication("owner-suspended").IsEnabled());
        }

        [TestMethod]
        public void GetApplication_正常なアプリケーションはインスタンスを返却()
        {
            Assert.AreNotEqual(null, _authorizeService.GetApplication("owner-normal"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAuthorizationCode_null値は例外発生()
        {
            _authorizeService.GetAuthorizationCode(null);
        }

        [TestMethod]
        public void GetAuthorizationCode_存在しない認可コードはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetAuthorizationCode(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void GetAuthorizationCode_使用済み認可コードを取得できる()
        {
            Assert.AreEqual(false, _authorizeService.GetAuthorizationCode("used").IsEnabled(_dateTimeProvider.Now));
        }

        [TestMethod]
        public void GetAuthorizationCode_正常な認可コードはインスタンスを返却()
        {
            Assert.AreNotEqual(null, _authorizeService.GetAuthorizationCode("normal"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRefreshToken_null値は例外発生()
        {
            _authorizeService.GetRefreshToken(null);
        }

        [TestMethod]
        public void GetRefreshToken_存在しないトークンはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetRefreshToken(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void GetRefreshToken_停止しているトークンを取得できる()
        {
            Assert.AreEqual(false, _authorizeService.GetRefreshToken("suspended").IsEnabled(_dateTimeProvider.Now));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetRefreshToken_PrincipalTypeが不明な値は例外発生()
        {
            _authorizeService.GetRefreshToken("exception");
        }

        [TestMethod]
        public void GetRefreshToken_Applicationが停止しているトークンはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetRefreshToken("app-suspended"));
        }

        [TestMethod]
        public void GetRefreshToken_Accountが停止しているトークンはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetRefreshToken("user-suspended"));
        }

        [TestMethod]
        public void GetRefreshToken_正常なアプリケーショントークンはインスタンスを返却()
        {
            Assert.AreNotEqual(null, _authorizeService.GetRefreshToken("normal"));
        }

        [TestMethod]
        public void GetRefreshToken_正常なユーザートークンはインスタンスを返却()
        {
            Assert.AreNotEqual(null, _authorizeService.GetRefreshToken("user"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAccessToken_null値は例外発生()
        {
            _authorizeService.GetAccessToken(null);
        }

        [TestMethod]
        public void GetAccessToken_存在しないトークンはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetAccessToken(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void GetAccessToken_停止しているトークンを取得できる()
        {
            Assert.AreEqual(false, _authorizeService.GetAccessToken("suspended").IsEnabled(_dateTimeProvider.Now));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetAccessToken_PrincipalTypeが不明な値は例外発生()
        {
            _authorizeService.GetAccessToken("exception");
        }

        [TestMethod]
        public void GetAccessToken_Applicationが停止しているトークンはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetAccessToken("app-suspended"));
        }

        [TestMethod]
        public void GetAccessToken_Accountが停止しているトークンはnullを返却()
        {
            Assert.AreEqual(null, _authorizeService.GetAccessToken("user-suspended"));
        }

        [TestMethod]
        public void GetAccessToken_正常なアプリケーショントークンはインスタンスを返却()
        {
            Assert.AreNotEqual(null, _authorizeService.GetAccessToken("normal"));
        }

        [TestMethod]
        public void GetAccessToken_正常なユーザートークンはインスタンスを返却()
        {
            Assert.AreNotEqual(null, _authorizeService.GetAccessToken("user"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAuthorizationCode_applicationIdに対するEmpty値は例外発生()
        {
            _authorizeService.CreateAuthorizationCode(Guid.Empty, Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAuthorizationCode_accountIdに対するEmpty値は例外発生()
        {
            _authorizeService.CreateAuthorizationCode(Guid.NewGuid(), Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAuthorizationCode_Applicationが無効な場合は例外発生()
        {
            _authorizeService.CreateAuthorizationCode(Guid.Parse("00000000-A002-0000-0000-000000000000"), Guid.Parse("00000000-B001-0000-0000-000000000000"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAuthorizationCode_Accountが無効な場合は例外発生()
        {
            _authorizeService.CreateAuthorizationCode(Guid.Parse("00000000-A001-0000-0000-000000000000"), Guid.Parse("00000000-B002-0000-0000-000000000000"));
        }

        [TestMethod]
        public void CreateAuthorizationCode_正常なパラメータの場合は期待通りのインスタンスを返却()
        {
            var authorizationCode = _authorizeService.CreateAuthorizationCode(Guid.Parse("00000000-A001-0000-0000-000000000000"), Guid.Parse("00000000-B001-0000-0000-000000000000"));
            Assert.AreEqual(AuthorizationCodeStatus.NORMAL, authorizationCode.Status);
            Assert.IsTrue(authorizationCode.CreateTime.AddSeconds(_appSettings.AuthorizationCodeExpiresIn) == authorizationCode.ExpiryTime);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAccessToken_refreshTokenに対するnull値は例外発生()
        {
            _authorizeService.CreateAccessToken(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_refreshTokenが無効の場合は例外発生()
        {
            var refreshToken = _authorizeService.GetRefreshToken("normal");
            refreshToken.Status = RefreshTokenStatus.SUSPENDED;
            _authorizeService.CreateAccessToken(refreshToken);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_refreshTokenのApplicationが無効時は例外発生()
        {
            var refreshToken = _authorizeService.GetRefreshToken("normal");
            refreshToken.ApplicationId = Guid.Parse("00000000-A002-0000-0000-000000000000");
            _authorizeService.CreateAccessToken(refreshToken);
        }

        [TestMethod]
        public void CreateAccessToken_refreshTokenが有効なパラメータの場合は期待通りのインスタンスを返却()
        {
            var refreshToken = _authorizeService.GetRefreshToken("normal");
            var accessToken = _authorizeService.CreateAccessToken(refreshToken);
            Assert.AreEqual(AccessTokenStatus.NORMAL, accessToken.Status);
            Assert.IsTrue(accessToken.CreateTime.AddSeconds(_appSettings.AccessTokenExpiresIn) == accessToken.ExpiryTime);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAccessToken_applicationIdに対するEmpty値は例外発生()
        {
            _authorizeService.CreateAccessToken(Guid.Empty, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_applicationIdに対する存在しない値は例外発生()
        {
            _authorizeService.CreateAccessToken(Guid.NewGuid(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_applicationId指定時にscopesに対する許可されていない値を指定すると例外発生()
        {
            _authorizeService.CreateAccessToken(Guid.Parse("00000000-A003-0000-0000-000000000000"), new string[] { Scopes.FullControl });
        }

        [TestMethod]
        public void CreateAccessToken_applicationId指定時にscopesに対する許可されている値を指定すると指定した値のみ返却する()
        {
            var (accessToken, refreshToken) = _authorizeService.CreateAccessToken(Guid.Parse("00000000-A001-0000-0000-000000000000"), new string[] { Scopes.Read });

            Assert.AreEqual(AccessTokenStatus.NORMAL, accessToken.Status);
            Assert.IsTrue(accessToken.CreateTime.AddSeconds(_appSettings.AccessTokenExpiresIn) == accessToken.ExpiryTime);
            Assert.AreEqual(1, accessToken.Scopes.Count);
            Assert.AreEqual(Scopes.Read, accessToken.Scopes[0]);

            Assert.AreEqual(RefreshTokenStatus.NORMAL, refreshToken.Status);
            Assert.IsTrue(refreshToken.CreateTime.AddSeconds(_appSettings.RefreshTokenExpiresIn) == refreshToken.ExpiryTime);
            Assert.AreEqual(1, refreshToken.Scopes.Count);
            Assert.AreEqual(Scopes.Read, refreshToken.Scopes[0]);
        }

        [TestMethod]
        public void CreateAccessToken_applicationId指定時にscopesに対するnull値を指定するとApplicationのscopes値を返却する()
        {
            var (accessToken, refreshToken) = _authorizeService.CreateAccessToken(Guid.Parse("00000000-A001-0000-0000-000000000000"), null);

            Assert.AreEqual(AccessTokenStatus.NORMAL, accessToken.Status);
            Assert.IsTrue(accessToken.CreateTime.AddSeconds(_appSettings.AccessTokenExpiresIn) == accessToken.ExpiryTime);
            Assert.AreEqual(3, accessToken.Scopes.Count);
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.Read));
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.Write));
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.FullControl));

            Assert.AreEqual(RefreshTokenStatus.NORMAL, refreshToken.Status);
            Assert.IsTrue(refreshToken.CreateTime.AddSeconds(_appSettings.RefreshTokenExpiresIn) == refreshToken.ExpiryTime);
            Assert.AreEqual(3, refreshToken.Scopes.Count);
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.Read));
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.Write));
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.FullControl));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAccessToken_authorizationCodeに対するnull値は例外発生()
        {
            _authorizeService.CreateAccessToken(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_authorizationCodeが使用済みの場合は例外発生()
        {
            var authorizationCode = _authorizeService.GetAuthorizationCode("normal");
            authorizationCode.Status = AuthorizationCodeStatus.USED;
            _authorizeService.CreateAccessToken(authorizationCode, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_authorizationCodeのApplicationが無効時は例外発生()
        {
            var authorizationCode = _authorizeService.GetAuthorizationCode("app-suspended");
            _authorizeService.CreateAccessToken(authorizationCode, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_authorizationCodeのAccountが無効時は例外発生()
        {
            var authorizationCode = _authorizeService.GetAuthorizationCode("user-suspended");
            _authorizeService.CreateAccessToken(authorizationCode, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateAccessToken_authorizationCode指定時にscopesに対する許可されていない値を指定すると例外発生()
        {
            var authorizationCode = _authorizeService.GetAuthorizationCode("user");
            _authorizeService.CreateAccessToken(authorizationCode, new string[] { Scopes.FullControl });
        }

        [TestMethod]
        public void CreateAccessToken_authorizationCode指定時にscopesに対する許可されている値を指定すると指定した値のみ返却する()
        {
            var authorizationCode = _authorizeService.GetAuthorizationCode("to-use1");
            var (accessToken, refreshToken) = _authorizeService.CreateAccessToken(authorizationCode, new string[] { Scopes.Read });

            Assert.AreEqual(AccessTokenStatus.NORMAL, accessToken.Status);
            Assert.IsTrue(accessToken.CreateTime.AddSeconds(_appSettings.AccessTokenExpiresIn) == accessToken.ExpiryTime);
            Assert.AreEqual(1, accessToken.Scopes.Count);
            Assert.AreEqual(Scopes.Read, accessToken.Scopes[0]);

            Assert.AreEqual(RefreshTokenStatus.NORMAL, refreshToken.Status);
            Assert.IsTrue(refreshToken.CreateTime.AddSeconds(_appSettings.RefreshTokenExpiresIn) == refreshToken.ExpiryTime);
            Assert.AreEqual(1, refreshToken.Scopes.Count);
            Assert.AreEqual(Scopes.Read, refreshToken.Scopes[0]);
        }

        [TestMethod]
        public void CreateAccessToken_authorizationCode指定時にscopesに対するnull値を指定するとAccountのscopes値を返却する()
        {
            var authorizationCode = _authorizeService.GetAuthorizationCode("to-use2");
            var (accessToken, refreshToken) = _authorizeService.CreateAccessToken(authorizationCode, null);

            Assert.AreEqual(AccessTokenStatus.NORMAL, accessToken.Status);
            Assert.IsTrue(accessToken.CreateTime.AddSeconds(_appSettings.AccessTokenExpiresIn) == accessToken.ExpiryTime);
            Assert.AreEqual(4, accessToken.Scopes.Count);
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.Read));
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.Write));
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.FullControl));
            Assert.IsTrue(accessToken.Scopes.Contains(Scopes.User));

            Assert.AreEqual(RefreshTokenStatus.NORMAL, refreshToken.Status);
            Assert.IsTrue(refreshToken.CreateTime.AddSeconds(_appSettings.RefreshTokenExpiresIn) == refreshToken.ExpiryTime);
            Assert.AreEqual(4, refreshToken.Scopes.Count);
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.Read));
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.Write));
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.FullControl));
            Assert.IsTrue(refreshToken.Scopes.Contains(Scopes.User));
        }
    }
}