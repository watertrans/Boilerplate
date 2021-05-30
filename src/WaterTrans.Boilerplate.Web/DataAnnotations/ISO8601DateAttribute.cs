using System;
using System.Globalization;
using WaterTrans.Boilerplate.Domain.Utils;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ISO8601DateAttribute : AdapteredValidationAttribute
    {
        public ISO8601DateAttribute()
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

            return DateUtil.IsISO8601Date((string)value);
        }
    }
}
