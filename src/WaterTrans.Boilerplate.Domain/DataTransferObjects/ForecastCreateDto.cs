using System;

namespace WaterTrans.Boilerplate.Domain.DataTransferObjects
{
    public class ForecastCreateDto
    {
        public string ForecastCode { get; set; }
        public string CountryCode { get; set; }
        public string CityCode { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public string Summary { get; set; }
    }
}
