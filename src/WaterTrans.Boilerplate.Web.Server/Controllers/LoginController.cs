using Microsoft.AspNetCore.Mvc;
using System.Web;
using WaterTrans.Boilerplate.Application.Abstractions.UseCases;
using WaterTrans.Boilerplate.Application.Constants;
using WaterTrans.Boilerplate.Web.Filters;
using WaterTrans.Boilerplate.Web.Resources;
using WaterTrans.Boilerplate.Web.Server.ViewModels;

namespace WaterTrans.Boilerplate.Web.Server.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginUseCase _loginUseCase;

        public LoginController(ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [RestoreModelState]
        public IActionResult Index()
        {
            return View(new LoginIndexViewModel());
        }

        [HttpPost]
        [StoreModelState]
        public IActionResult Index(LoginIndexViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var result = _loginUseCase.Login("clientapp", request.LoginId, request.Password);

            if (result.State == LoginState.Success)
            {
                return Redirect("Dashboard?code=" + HttpUtility.UrlEncode(result.AuthorizationCode.Code));
            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.ErrorResultLoginFailed);
                return RedirectToAction("Index");
            }
        }
    }
}
