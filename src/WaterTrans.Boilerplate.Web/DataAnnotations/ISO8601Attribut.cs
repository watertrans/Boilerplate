using System;
using System.Globalization;
using WaterTrans.Boilerplate.Domain.Utils;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ISO8601Attribute : AdapteredValidationAttribute
    {
        public ISO8601Attribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string))
            {
                return false;
            }

            return DateUtil.IsISO8601((string)value);
        }
    }
}
