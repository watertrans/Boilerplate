namespace WaterTrans.Boilerplate.Web.ResponseObjects
{
    public class BaseError
    {
        public string Code { get; set; }
        public BaseError InnerError { get; set; }
    }
}
