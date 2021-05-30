using System;
using System.Collections.Generic;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DuplicatedStringArrayAttribute : AdapteredValidationAttribute
    {
        public DuplicatedStringArrayAttribute()
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

            var list = new List<string>();

            foreach (var str in value as IEnumerable<string>)
            {
                if (list.Contains(str))
                {
                    return false;
                }

                list.Add(str);
            }

            return true;
        }
    }
}
