using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Domain.ValueObjects;
using WaterTrans.Boilerplate.Tests;

namespace WaterTrans.Boilerplate.Persistence.Repositories.IntegrationTests
{
    [TestClass]
    [TestCategory("IntegrationTests")]
    public class ForecastRepositoryTest
    {
        [TestMethod]
        public void Create_登録時に例外が発生しない()
        {
            var forecast = new Forecast
            {
                ForecastId = Guid.NewGuid(),
                ForecastCode = "01234567890123456789",
                City = new City("BOS"),
                Country = new Country("USA"),
                Date = DateTime.Today,
                Summary = new string('A', 100),
                Temperature = int.MaxValue,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            forecastRepository.Create(forecast);
        }

        [TestMethod]
        public void GetById_存在するデータを取得できる()
        {
            var forecastKey = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecast = forecastRepository.GetById(forecastKey);

            Assert.IsNotNull(forecast);
        }

        [TestMethod]
        public void GetById_存在しないデータを取得するとNull()
        {
            var forecastKey = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            var forecast = forecastRepository.GetById(forecastKey);

            Assert.IsNull(forecast);
        }

        [TestMethod]
        public void Update_登録したデータを更新できる()
        {
            var forecast = new Forecast
            {
                ForecastId = Guid.NewGuid(),
                ForecastCode = "UPDATE",
                City = new City("BOS"),
                Country = new Country("USA"),
                Date = DateTime.Today,
                Summary = new string('A', 100),
                Temperature = int.MaxValue,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            forecastRepository.Create(forecast);
            forecast.UpdateTime = DateUtil.Now;
            Assert.IsTrue(forecastRepository.Update(forecast));
        }

        [TestMethod]
        public void Delete_登録したデータを削除できる()
        {
            var forecast = new Forecast
            {
                ForecastId = Guid.NewGuid(),
                ForecastCode = "DELETE",
                City = new City("BOS"),
                Country = new Country("USA"),
                Date = DateTime.Today,
                Summary = new string('A', 100),
                Temperature = int.MaxValue,
                CreateTime = DateTimeOffset.MaxValue,
                UpdateTime = DateTimeOffset.MaxValue,
            };
            var forecastRepository = new ForecastRepository(TestEnvironment.DBSettings);
            forecastRepository.Create(forecast);
            Assert.IsTrue(forecastRepository.Delete(forecast.ForecastId));
        }
    }
}
