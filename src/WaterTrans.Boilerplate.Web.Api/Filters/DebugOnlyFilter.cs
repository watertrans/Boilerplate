using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WaterTrans.Boilerplate.Domain.Abstractions;
using WaterTrans.Boilerplate.Web.Api.ObjectResults;

namespace WaterTrans.Boilerplate.Web.Api.Filters
{
    public class DebugOnlyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var envSettings = filterContext.HttpContext.RequestServices.GetService<IEnvSettings>();
            if (!envSettings.IsDebug)
            {
                filterContext.Result = ErrorObjectResultFactory.Forbidden();
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
