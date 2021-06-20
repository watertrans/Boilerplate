using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WaterTrans.Boilerplate.Web.Filters
{
    public class StoreModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                if (filterContext.Result is RedirectResult
                    || filterContext.Result is RedirectToRouteResult
                    || filterContext.Result is RedirectToActionResult)
                {
                    var controller = filterContext.Controller as Controller;
                    if (controller != null && filterContext.ModelState != null)
                    {
                        var modelState = ModelStateHelpers.SerializeModelState(filterContext.ModelState);
                        // TODO TempDataをSessionに変更＆キー名を正しくする
                        controller.TempData["PRG_ModelState"] = modelState;
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
