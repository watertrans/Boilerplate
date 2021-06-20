using System.ComponentModel.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.Server.ViewModels
{
    public class LoginIndexViewModel
    {
        [Required(ErrorMessage = "DataAnnotationRequired")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "DataAnnotationRequired")]
        public string Password { get; set; }
    }
}
