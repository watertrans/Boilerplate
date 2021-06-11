using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Exceptions;
using WaterTrans.Boilerplate.Persistence.Exceptions;
using WaterTrans.Boilerplate.Persistence.Repositories;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Domain.Services.IntegrationTests
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

            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastRepository);
            forecastService.Create(createForecastDto);
            forecastService.Create(createForecastDto);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Delete_���݂��Ȃ��L�[�ō폜������ƃG���[()
        {
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastRepository);
            forecastService.Delete(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetById_���݂��Ȃ��L�[�Ŏ擾������ƃG���[()
        {
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastRepository);
            forecastService.GetById(Guid.NewGuid());
        }
    }
}
