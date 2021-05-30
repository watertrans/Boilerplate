using System;
using System.Collections.Generic;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredStringArrayAttribute : AdapteredValidationAttribute
    {
        public RequiredStringArrayAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is IEnumerable<string>))
            {
                return false;
            }

            foreach (var str in value as IEnumerable<string>)
            {
                if (str == null || str == string.Empty)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
