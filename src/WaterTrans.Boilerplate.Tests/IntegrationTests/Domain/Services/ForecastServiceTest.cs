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
        public void Create_重複した値を登録をするとエラー()
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
        public void Delete_存在しないキーで削除をするとエラー()
        {
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastRepository);
            forecastService.Delete(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetById_存在しないキーで取得をするとエラー()
        {
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecastService = new ForecastService(forecastRepository);
            forecastService.GetById(Guid.NewGuid());
        }
    }
}
