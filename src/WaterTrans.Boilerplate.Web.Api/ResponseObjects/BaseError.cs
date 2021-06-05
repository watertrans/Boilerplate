namespace WaterTrans.Boilerplate.Web.Api.ResponseObjects
{
    public class BaseError
    {
        public string Code { get; set; }
        public BaseError InnerError { get; set; }
    }
}
