using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WaterTrans.Boilerplate.Web.Api.ResponseObjects;

namespace WaterTrans.Boilerplate.Web.Api.ObjectResults
{
    [DefaultStatusCode(DefaultStatusCode)]
    public class ErrorObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status400BadRequest;
        public ErrorObjectResult([ActionResultObjectValue] object value)
            : base(value)
        {
            StatusCode = DefaultStatusCode;
        }
        public Error Error
        {
            get { return (Error)Value; }
        }
    }
}
