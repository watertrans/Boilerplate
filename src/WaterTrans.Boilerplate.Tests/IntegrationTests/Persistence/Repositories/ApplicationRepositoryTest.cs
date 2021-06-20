using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WaterTrans.Boilerplate.Domain.Constants;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class ApplicationRepositoryTest
    {
        [TestMethod]
        public void Create_�o�^���ɗ�O���������Ȃ�()
        {
            var now = TestEnvironment.DateTimeProvider.Now;
            var application = new Domain.Entities.Application
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = new string('X', 100),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = new List<string>(new string[] { Roles.Owner }),
                Scopes = new List<string>(new string[] { Scopes.FullControl }),
                GrantTypes = new List<string>(new string[] { GrantTypes.ClientCredentials }),
                RedirectUris = new List<string>(),
                PostLogoutRedirectUris = new List<string>(),
                Status = ApplicationStatus.NORMAL,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var applicationRepository = new ApplicationRepository(TestEnvironment.DBSettings);
            applicationRepository.Create(application);
        }

        [TestMethod]
        public void GetById_���݂���f�[�^���擾�ł���()
        {
            var applicationRepository = new ApplicationRepository(TestEnvironment.DBSettings);
            var application = applicationRepository.GetById(Guid.Parse("00000000-A001-0000-0000-000000000000"));
            Assert.IsNotNull(application);
        }

        [TestMethod]
        public void Update_�o�^�����f�[�^���X�V�ł���()
        {
            var applicationRepository = new ApplicationRepository(TestEnvironment.DBSettings);
            var application = applicationRepository.GetById(Guid.Parse("00000000-A001-0000-0000-000000000000"));
            application.UpdateTime = TestEnvironment.DateTimeProvider.Now;
            Assert.IsTrue(applicationRepository.Update(application));
        }

        [TestMethod]
        public void Delete_�o�^�����f�[�^���폜�ł���()
        {
            var now = TestEnvironment.DateTimeProvider.Now;
            var application = new Domain.Entities.Application
            {
                ApplicationId = Guid.NewGuid(),
                ClientId = new string('Y', 100),
                ClientSecret = new string('X', 100),
                Name = new string('X', 100),
                Description = new string('X', 400),
                Roles = new List<string>(new string[] { Roles.Owner }),
                Scopes = new List<string>(new string[] { Scopes.FullControl }),
                GrantTypes = new List<string>(new string[] { GrantTypes.ClientCredentials }),
                RedirectUris = new List<string>(),
                PostLogoutRedirectUris = new List<string>(),
                Status = ApplicationStatus.SUSPENDED,
                CreateTime = DateTime.MaxValue,
                UpdateTime = DateTime.MaxValue,
            };
            var applicationRepository = new ApplicationRepository(TestEnvironment.DBSettings);
            applicationRepository.Create(application);
            Assert.IsTrue(applicationRepository.Delete(application.ApplicationId));
        }
    }
}
