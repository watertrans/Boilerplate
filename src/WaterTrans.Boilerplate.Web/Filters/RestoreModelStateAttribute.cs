using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WaterTrans.Boilerplate.Web.Filters
{
    public class RestoreModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;

            // TODO TempDataをSessionに変更＆キー名を正しくする
            var serializedModelState = controller?.TempData["PRG_ModelState"] as string;

            if (serializedModelState != null)
            {
                var modelState = ModelStateHelpers.DeserializeModelState(serializedModelState);
                context.ModelState.Merge(modelState);
            }

            base.OnActionExecuting(context);
        }
    }
}
