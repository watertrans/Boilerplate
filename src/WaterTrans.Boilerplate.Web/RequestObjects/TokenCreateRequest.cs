using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.RequestObjects
{
    public class TokenCreateRequest
    {
        [Display(Name = "DisplayCommonGrantType")]
        [Required(ErrorMessage = "DataAnnotationRequired")]
        [ModelBinder(Name = "grant_type")]
        public string GrantType { get; set; }

        [ModelBinder(Name = "scope")]
        public string Scope { get; set; }

        [ModelBinder(Name = "client_id")]
        public string ClientId { get; set; }

        [ModelBinder(Name = "client_secret")]
        public string ClientSecret { get; set; }

        [ModelBinder(Name = "code")]
        public string Code { get; set; }
    }
}
