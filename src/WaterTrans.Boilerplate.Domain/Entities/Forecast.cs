using System;
using WaterTrans.Boilerplate.Domain.DataTransferObjects;
using WaterTrans.Boilerplate.Domain.ValueObjects;

namespace WaterTrans.Boilerplate.Domain.Entities
{
    public class Forecast : Entity
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
    }
}
