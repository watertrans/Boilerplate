using WaterTrans.Boilerplate.Domain.ValueObjects;

namespace WaterTrans.Boilerplate.Web.Api.ResponseObjects
{
    public class ForecastResponse
    {
        public string ForecastId { get; set; }
        public string ForecastCode { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public string Date { get; set; }
        public int Temperature { get; set; }
        public string Summary { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
    }
}
