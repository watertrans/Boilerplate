using System.Collections.Generic;

namespace WaterTrans.Boilerplate.Web.ResponseObjects
{
    public class Error : BaseError
    {
        public string Message { get; set; }
        public List<Error> Details { get; set; }
        public string Target { get; set; }
    }
}
