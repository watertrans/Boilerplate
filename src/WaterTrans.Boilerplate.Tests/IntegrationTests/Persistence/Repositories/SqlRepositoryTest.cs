using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Persistence.Exceptions;
using WaterTrans.Boilerplate.Persistence.SqlEntities;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class SqlRepositoryTest
    {
        private readonly SqlRepository<ApplicationSqlEntity> _sqlRepository;

        public SqlRepositoryTest()
        {
            _sqlRepository = new SqlRepository<ApplicationSqlEntity>(TestEnvironment.DBSettings);
        }

        [TestMethod]
        public void Create_�o�^���ɗ�O���������Ȃ�()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            _sqlRepository.Create(applicationSqlEntity);

            Assert.IsTrue(applicationSqlEntity.ConcurrencyToken != DateTime.MinValue);
        }

        [TestMethod]
        public async Task CreateAsync_�o�^���ɗ�O���������Ȃ�()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            await _sqlRepository.CreateAsync(applicationSqlEntity);

            Assert.IsTrue(applicationSqlEntity.ConcurrencyToken != DateTime.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void Create_�d���o�^���ɗ�O����()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            _sqlRepository.Create(applicationSqlEntity);
            _sqlRepository.Create(applicationSqlEntity);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public async Task CreateAsync_�d���o�^���ɗ�O����()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            await _sqlRepository.CreateAsync(applicationSqlEntity);
            await _sqlRepository.CreateAsync(applicationSqlEntity);
        }

        [TestMethod]
        public void GetAll_��O���������Ȃ�()
        {
            _sqlRepository.GetAll();
        }

        [TestMethod]
        public async Task GetAllAsync_��O���������Ȃ�()
        {
            await _sqlRepository.GetAllAsync();
        }

        [TestMethod]
        public void GetById_��O���������Ȃ�()
        {
            var sqlEntity = _sqlRepository.GetById(new ApplicationSqlEntity { ApplicationId = Guid.Parse("00000000-a001-0000-0000-000000000000") });
            Assert.IsNotNull(sqlEntity);
        }

        [TestMethod]
        public async Task GetByIdAsync_��O���������Ȃ�()
        {
            var sqlEntity = await _sqlRepository.GetByIdAsync(new ApplicationSqlEntity { ApplicationId = Guid.Parse("00000000-a001-0000-0000-000000000000") });
            Assert.IsNotNull(sqlEntity);
        }

        [TestMethod]
        public void Update_�X�V���ɗ�O���������Ȃ�()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            _sqlRepository.Create(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = DateUtil.Now;

            Assert.IsTrue(_sqlRepository.Update(applicationSqlEntity));
        }

        [TestMethod]
        public async Task UpdateAsync_�X�V���ɗ�O���������Ȃ�()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            await _sqlRepository.CreateAsync(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = DateUtil.Now;

            Assert.IsTrue(await _sqlRepository.UpdateAsync(applicationSqlEntity));
        }

        [TestMethod]
        public void Update_�������s�g�[�N���̕ω��ɂ��X�V���s()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            _sqlRepository.Create(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = DateUtil.Now;
            applicationSqlEntity.ConcurrencyToken = applicationSqlEntity.ConcurrencyToken.AddSeconds(-1);

            Assert.IsFalse(_sqlRepository.Update(applicationSqlEntity));
        }

        [TestMethod]
        public async Task UpdateAsync_�������s�g�[�N���̕ω��ɂ��X�V���s()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            await _sqlRepository.CreateAsync(applicationSqlEntity);

            applicationSqlEntity.Name = new string('Y', 100);
            applicationSqlEntity.UpdateTime = DateUtil.Now;
            applicationSqlEntity.ConcurrencyToken = applicationSqlEntity.ConcurrencyToken.AddSeconds(-1);

            Assert.IsFalse(await _sqlRepository.UpdateAsync(applicationSqlEntity));
        }

        [TestMethod]
        public void Delete_�폜���ɗ�O���������Ȃ�()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            _sqlRepository.Create(applicationSqlEntity);
            Assert.IsTrue(_sqlRepository.Delete(applicationSqlEntity));
        }

        [TestMethod]
        public async Task DeleteAsync_�X�V���ɗ�O���������Ȃ�()
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
                CreateTime = DateUtil.Now,
                UpdateTime = DateUtil.Now,
            };

            await _sqlRepository.CreateAsync(applicationSqlEntity);
            Assert.IsTrue(await _sqlRepository.DeleteAsync(applicationSqlEntity));
        }
    }
}
