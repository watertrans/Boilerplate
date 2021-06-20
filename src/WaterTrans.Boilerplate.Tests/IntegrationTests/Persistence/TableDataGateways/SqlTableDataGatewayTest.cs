using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Persistence.Exceptions;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.TableDataGateways.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class SqlTableDataGatewayTest
    {
        private readonly SqlTableDataGateway<ApplicationSqlEntity> _sqlTableDataGateway;

        public SqlTableDataGatewayTest()
        {
            _sqlTableDataGateway = new SqlTableDataGateway<ApplicationSqlEntity>(TestEnvironment.DBSettings);
        }

        [TestMethod]
        public void Create_登録時に例外が発生しない()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            _sqlTableDataGateway.Create(applicationSqlEntity);

            Assert.IsTrue(applicationSqlEntity.ConcurrencyToken != DateTime.MinValue);
        }

        [TestMethod]
        public async Task CreateAsync_登録時に例外が発生しない()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            await _sqlTableDataGateway.CreateAsync(applicationSqlEntity);

            Assert.IsTrue(applicationSqlEntity.ConcurrencyToken != DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void Create_重複登録時に例外する()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            _sqlTableDataGateway.Create(applicationSqlEntity);
            _sqlTableDataGateway.Create(applicationSqlEntity);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public async Task CreateAsync_重複登録時に例外する()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            await _sqlTableDataGateway.CreateAsync(applicationSqlEntity);
            await _sqlTableDataGateway.CreateAsync(applicationSqlEntity);
        }

        [TestMethod]
        public void GetAll_例外が発生しない()
        {
            _sqlTableDataGateway.GetAll();
        }

        [TestMethod]
        public async Task GetAllAsync_例外が発生しない()
        {
            await _sqlTableDataGateway.GetAllAsync();
        }

        [TestMethod]
        public void GetById_例外が発生しない()
        {
            var sqlEntity = _sqlTableDataGateway.GetById(new ApplicationSqlEntity { ApplicationId = Guid.Parse("00000000-a001-0000-0000-000000000000") });
            Assert.IsNotNull(sqlEntity);
        }

        [TestMethod]
        public async Task GetByIdAsync_例外が発生しない()
        {
            var sqlEntity = await _sqlTableDataGateway.GetByIdAsync(new ApplicationSqlEntity { ApplicationId = Guid.Parse("00000000-a001-0000-0000-000000000000") });
            Assert.IsNotNull(sqlEntity);
        }

        [TestMethod]
        public void Update_更新時に例外が発生しない()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            _sqlTableDataGateway.Create(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = TestEnvironment.DateTimeProvider.Now;

            Assert.IsTrue(_sqlTableDataGateway.Update(applicationSqlEntity));
        }

        [TestMethod]
        public async Task UpdateAsync_更新時に例外が発生しない()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            await _sqlTableDataGateway.CreateAsync(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = TestEnvironment.DateTimeProvider.Now;

            Assert.IsTrue(await _sqlTableDataGateway.UpdateAsync(applicationSqlEntity));
        }

        [TestMethod]
        public void Update_同時実行トークンの変化による更新失敗()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            _sqlTableDataGateway.Create(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = TestEnvironment.DateTimeProvider.Now;
            applicationSqlEntity.ConcurrencyToken = applicationSqlEntity.ConcurrencyToken.AddSeconds(-1);

            Assert.IsFalse(_sqlTableDataGateway.Update(applicationSqlEntity));
        }

        [TestMethod]
        public async Task UpdateAsync_同時実行トークンの変化による更新失敗()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            await _sqlTableDataGateway.CreateAsync(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = TestEnvironment.DateTimeProvider.Now;
            applicationSqlEntity.ConcurrencyToken = applicationSqlEntity.ConcurrencyToken.AddSeconds(-1);

            Assert.IsFalse(await _sqlTableDataGateway.UpdateAsync(applicationSqlEntity));
        }

        [TestMethod]
        public void Delete_削除時に例外が発生しない()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            _sqlTableDataGateway.Create(applicationSqlEntity);
            Assert.IsTrue(_sqlTableDataGateway.Delete(applicationSqlEntity));
        }

        [TestMethod]
        public async Task DeleteAsync_更新時に例外が発生しない()
        {
            var applicationSqlEntity = new ApplicationSqlEntity
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = Guid.NewGuid().ToString(),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = "[]",
                GrantTypes = "[]",
                PostLogoutRedirectUris = "[]",
                RedirectUris = "[]",
                Scopes = "[]",
                Status = ApplicationStatus.NORMAL.ToString(),
                CreateTime = TestEnvironment.DateTimeProvider.Now,
                UpdateTime = TestEnvironment.DateTimeProvider.Now,
            };

            await _sqlTableDataGateway.CreateAsync(applicationSqlEntity);
            Assert.IsTrue(await _sqlTableDataGateway.DeleteAsync(applicationSqlEntity));
        }
    }
}
