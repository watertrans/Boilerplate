using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WaterTrans.Boilerplate.Web.Api.ObjectResults;

namespace WaterTrans.Boilerplate.Web.Api.Filters
{
    public class CatchAllExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CatchAllExceptionFilter> _logger;
        public CatchAllExceptionFilter(ILogger<CatchAllExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "InternalServerError");
            context.Result = ErrorObjectResultFactory.InternalServerError();
            context.ExceptionHandled = true;
            return;
        }
    }
}
