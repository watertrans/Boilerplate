using FluentValidation;
using FluentValidation.Results;
using System;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.Utils;
using WaterTrans.Boilerplate.Domain.ValueObjects;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class Forecast : Entity, IValidatableObject<Forecast>
    {
        public Forecast()
        {
        }

        public Forecast(ForecastCreateDto dto)
        {
            ForecastId = Guid.NewGuid();
            ForecastCode = dto.ForecastCode;
            Country = new Country(dto.CountryCode);
            City = new City(dto.CityCode);
            Date = dto.Date;
            Temperature = dto.Temperature;
            Summary = dto.Summary;
            CreateTime = DateUtil.Now;
            UpdateTime = DateUtil.Now;
        }

        public Guid ForecastId { get; set; }
        public string ForecastCode { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public string Summary { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime ConcurrencyToken { get; set; }
        public IValidator<Forecast> Validator { get; } = new ForecastValidator();

        public ValidationResult Validate()
        {
            return Validator.Validate(this);
        }

        public void ValidateAndThrow()
        {
            Validator.ValidateAndThrow(this);
        }

        private class ForecastValidator : AbstractValidator<Forecast>
        {
            internal ForecastValidator()
            {
                RuleFor(x => x.ForecastCode).Cascade(CascadeMode.Stop).NotNull().NotEmpty().Length(1, 20);
                RuleFor(x => x.Country).NotNull();
                RuleFor(x => x.City).NotNull();
                RuleFor(x => x.Summary).Cascade(CascadeMode.Stop).NotNull().NotEmpty().Length(1, 100);
                RuleFor(x => x.Temperature).GreaterThanOrEqualTo(-99).LessThanOrEqualTo(99);
            }
        }
    }
}
