using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class StringLengthArrayAttribute : StringLengthAttribute
    {
        public StringLengthArrayAttribute(int maximumLength)
            : base(maximumLength)
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
                return base.IsValid(str);
            }

            return true;
        }
    }
}
