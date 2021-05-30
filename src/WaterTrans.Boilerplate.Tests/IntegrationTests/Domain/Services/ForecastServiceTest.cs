using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Exceptions;
using WaterTrans.Boilerplate.Domain.Services;
using WaterTrans.Boilerplate.Persistence.Exceptions;
using WaterTrans.Boilerplate.Persistence.QueryServices;
using WaterTrans.Boilerplate.Persistence.Repositories;

namespace WaterTrans.Boilerplate.Tests.IntegrationTests.Domain.Services
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class ForecastServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void Create_�d�������l��o�^������ƃG���[()
        {
            var today = DateTime.Today;
            var createForecastDto = new ForecastCreateDto
            {
                ForecastCode = "Duplicate",
                CityCode = "TYO",
                CountryCode = "JPN",
                Date = today,
                Summary = "Hot",
                Temperature = 30,
            };

            var forecastQueryServiceMock = new ForecastQueryService(TestEnvironment.DBSettings);
            var forecastRepositoryMock = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastQueryServiceMock, forecastRepositoryMock);
            forecastService.Create(createForecastDto);
            forecastService.Create(createForecastDto);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Delete_���݂��Ȃ��L�[�ō폜������ƃG���[()
        {
            var forecastQueryServiceMock = new ForecastQueryService(TestEnvironment.DBSettings);
            var forecastRepositoryMock = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastQueryServiceMock, forecastRepositoryMock);
            forecastService.Delete(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Read_���݂��Ȃ��L�[�Ŏ擾������ƃG���[()
        {
            var forecastQueryServiceMock = new ForecastQueryService(TestEnvironment.DBSettings);
            var forecastRepositoryMock = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastQueryServiceMock, forecastRepositoryMock);
            forecastService.GetById(Guid.NewGuid());
        }
    }
}
