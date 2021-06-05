using System;
using System.ComponentModel.DataAnnotations;
using WaterTrans.Boilerplate.Web.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.Api.RequestObjects
{
    public class ForecastUpdateRequest
    {
        [Display(Name = "DisplayForecastForecastCode")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [StringLength(20, ErrorMessage = "DataAnnotationStringLength")]
        public string ForecastCode { get; set; }

        [Display(Name = "DisplayForecastCountryCode")]
        [StringLength(3, ErrorMessage = "DataAnnotationStringLength")]
        public string CountryCode { get; set; }

        [Display(Name = "DisplayForecastCityCode")]
        [StringLength(3, ErrorMessage = "DataAnnotationStringLength")]
        public string CityCode { get; set; }

        [Display(Name = "DisplayForecastDate")]
        [ISO8601Date(ErrorMessage = "DataAnnotationISO8601Date")]
        public string Date { get; set; }

        [Display(Name = "DisplayForecastTemperature")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "DataAnnotationRange")]
        public int? Temperature { get; set; }

        [Display(Name = "DisplayForecastSummary")]
        [StringLength(100, ErrorMessage = "DataAnnotationStringLength")]
        public string Summary { get; set; }

        [Display(Name = "DisplayCommonConcurrencyToken")]
        [ISO8601(ErrorMessage = "DataAnnotationISO8601")]
        public string ConcurrencyToken { get; set; }
    }
}
