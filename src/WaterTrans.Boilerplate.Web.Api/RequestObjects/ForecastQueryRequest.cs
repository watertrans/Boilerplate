using System.ComponentModel.DataAnnotations;
using WaterTrans.Boilerplate.Web.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.Api.RequestObjects
{
    public class ForecastQueryRequest
    {
        [Display(Name = "DisplayCommonPage")]
        [Range(1, 999, ErrorMessage = "DataAnnotationRange")]
        public int? Page { get; set; }

        [Display(Name = "DisplayCommonPageSize")]
        [Range(1, 100, ErrorMessage = "DataAnnotationRange")]
        public int? PageSize { get; set; }

        [Display(Name = "DisplayCommonQuery")]
        [StringLength(256, ErrorMessage = "DataAnnotationStringLength")]
        public string Query { get; set; }

        [Display(Name = "DisplayCommonSort")]
        [StringLength(100, ErrorMessage = "DataAnnotationStringLength")]
        [Sort("Date", "CountryCode", "CityCode", "CreateTime", ErrorMessage = "DataAnnotationSort")]
        public string Sort { get; set; }
    }
}
