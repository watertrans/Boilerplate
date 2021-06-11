using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaterTrans.Boilerplate.Domain.Entities;

namespace WaterTrans.Boilerplate.Entities.Services.UnitTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class ForecastTest
    {
        [TestMethod]
        public void Validate_null‚ª‹–‰Â‚³‚ê‚È‚¢€–Ú()
        {
            var forecast = new Forecast
            {
                ForecastCode = null,
                City = null,
                Country = null,
                Summary = null,
            };

            var result = forecast.Validator.TestValidate(forecast);
            result.ShouldHaveValidationErrorFor(x => x.ForecastCode);
            result.ShouldHaveValidationErrorFor(x => x.City);
            result.ShouldHaveValidationErrorFor(x => x.Country);
            result.ShouldHaveValidationErrorFor(x => x.Summary);
        }

        [TestMethod]
        public void Validate_‹ó•¶š‚ª‹–‰Â‚³‚ê‚È‚¢€–Ú()
        {
            var forecast = new Forecast
            {
                ForecastCode = string.Empty,
                Summary = string.Empty,
            };

            var result = forecast.Validator.TestValidate(forecast);
            result.ShouldHaveValidationErrorFor(x => x.ForecastCode);
            result.ShouldHaveValidationErrorFor(x => x.Summary);
        }

        [TestMethod]
        public void Validate_•¶š”‚ª”ÍˆÍŠO‚Ì€–Ú()
        {
            var forecast = new Forecast
            {
                ForecastCode = new string('0', 21),
                Summary = new string('0', 101),
            };

            var result = forecast.Validator.TestValidate(forecast);
            result.ShouldHaveValidationErrorFor(x => x.ForecastCode);
            result.ShouldHaveValidationErrorFor(x => x.Summary);
        }

        [DataTestMethod]
        [DataRow(-100)]
        [DataRow(100)]
        public void Validate_”’l‚ª”ÍˆÍŠO‚Ì€–Ú(int temperature)
        {
            var forecast = new Forecast
            {
                Temperature = temperature,
            };

            var result = forecast.Validator.TestValidate(forecast);
            result.ShouldHaveValidationErrorFor(x => x.Temperature);
        }
    }
}
