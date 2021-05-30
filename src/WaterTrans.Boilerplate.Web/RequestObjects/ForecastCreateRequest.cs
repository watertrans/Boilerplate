using System.ComponentModel.DataAnnotations;
using WaterTrans.Boilerplate.Web.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.RequestObjects
{
    public class ForecastCreateRequest
    {
        [Display(Name = "DisplayForecastForecastCode")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [StringLength(20, ErrorMessage = "DataAnnotationStringLength")]
        public string ForecastCode { get; set; }

        [Display(Name = "DisplayForecastCountryCode")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [StringLength(3, ErrorMessage = "DataAnnotationStringLength")]
        public string CountryCode { get; set; }

        [Display(Name = "DisplayForecastCityCode")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [StringLength(3, ErrorMessage = "DataAnnotationStringLength")]
        public string CityCode { get; set; }

        [Display(Name = "DisplayForecastDate")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [ISO8601Date(ErrorMessage = "DataAnnotationDate")]
        public string Date { get; set; }

        [Display(Name = "DisplayForecastTemperature")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [Range(-99, 99, ErrorMessage = "DataAnnotationRange")]
        public int Temperature { get; set; }

        [Display(Name = "DisplayForecastSummary")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [StringLength(100, ErrorMessage = "DataAnnotationStringLength")]
        public string Summary { get; set; }
    }
}
