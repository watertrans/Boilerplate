using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WaterTrans.Boilerplate.CrossCuttingConcerns.Exceptions;
using WaterTrans.Boilerplate.Domain.Abstractions.Repositories;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Entities;
using WaterTrans.Boilerplate.UnitTests;

namespace WaterTrans.Boilerplate.Domain.Services.UnitTests
{
    [TestClass]
    public class ForecastServiceTest
    {
        [TestMethod]
        public void Create_設定した値と同一の値が応答()
        {
            var today = DateTime.Today;
            var forecastCreateDto = new ForecastCreateDto
            {
                ForecastCode = "0000-0000-0000-0000",
                CityCode = "TYO",
                CountryCode = "JPN",
                Date = today,
                Summary = "Hot",
                Temperature = 30,
            };

            var forecastRepositoryMock = new Mock<IForecastRepository>();
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            var forecast = forecastService.Create(forecastCreateDto);

            forecastRepositoryMock.Verify(x => x.Create(It.IsAny<Forecast>()));
            Assert.AreEqual(forecastCreateDto.ForecastCode, forecast.ForecastCode);
            Assert.AreEqual(forecastCreateDto.CityCode, forecast.City.CityCode);
            Assert.AreEqual(forecastCreateDto.CountryCode, forecast.Country.CountryCode);
            Assert.AreEqual(forecastCreateDto.Date, today);
            Assert.AreEqual(forecastCreateDto.Summary, forecast.Summary);
            Assert.AreEqual(forecastCreateDto.Temperature, forecast.Temperature);
        }

        [TestMethod]
        public void Delete_存在するIDの場合は例外が発生しない()
        {
            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(true);
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            forecastService.Delete(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Delete_存在しないIDの場合は例外が発生する()
        {
            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(false);
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            forecastService.Delete(Guid.NewGuid());
        }

        [TestMethod]
        public void GetById_存在するIDの場合は例外が発生しない()
        {
            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Forecast());
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            var forecast = forecastService.GetById(Guid.NewGuid());
            Assert.IsNotNull(forecast);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void GetById_存在しないIDの場合は例外が発生する()
        {
            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Forecast)null);
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            var forecast = forecastService.GetById(Guid.NewGuid());
            Assert.IsNull(forecast);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Update_存在しないIDの場合は例外が発生する()
        {
            var today = DateTime.Today;
            var now = TestEnvironment.DateTimeProvider.Now;
            var forecastUpdateDto = new ForecastUpdateDto
            {
                ForecastId = Guid.NewGuid(),
                ForecastCode = "0000-0000-0000-0000",
                CityCode = "TYO",
                CountryCode = "JPN",
                Date = today,
                Summary = "Hot",
                Temperature = 30,
                ConcurrencyToken = now,
            };

            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Forecast)null);
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            var forecast = forecastService.Update(forecastUpdateDto);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityConcurrencyException))]
        public void Update_更新日が一致しない場合は例外が発生する()
        {
            var today = DateTime.Today;
            var now = TestEnvironment.DateTimeProvider.Now;
            var forecastUpdateDto = new ForecastUpdateDto
            {
                ForecastId = Guid.NewGuid(),
                ForecastCode = "0000-0000-0000-0000",
                CityCode = "TYO",
                CountryCode = "JPN",
                Date = today,
                Summary = "Hot",
                Temperature = 30,
                ConcurrencyToken = now,
            };
            var mockForecast = new Forecast
            {
                ForecastId = forecastUpdateDto.ForecastId,
                UpdateTime = TestEnvironment.DateTimeProvider.Now.AddSeconds(1),
            };

            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(mockForecast);
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            var forecast = forecastService.Update(forecastUpdateDto);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundException))]
        public void Update_更新対象が存在しなくなった場合は例外が発生する()
        {
            var today = DateTime.Today;
            var now = TestEnvironment.DateTimeProvider.Now;
            var forecastUpdateDto = new ForecastUpdateDto
            {
                ForecastId = Guid.NewGuid(),
                ForecastCode = "0000-0000-0000-0000",
                CityCode = "TYO",
                CountryCode = "JPN",
                Date = today,
                Summary = "Hot",
                Temperature = 30,
                ConcurrencyToken = now,
            };
            var mockForecast = new Forecast
            {
                ForecastId = forecastUpdateDto.ForecastId,
                UpdateTime = now,
            };

            var forecastRepositoryMock = new Mock<IForecastRepository>();
            forecastRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(mockForecast);
            forecastRepositoryMock.Setup(x => x.Update(It.IsAny<Forecast>())).Returns(false);
            var forecastService = new ForecastService(forecastRepositoryMock.Object, TestEnvironment.DateTimeProvider);
            var forecast = forecastService.Update(forecastUpdateDto);
        }
    }
}
