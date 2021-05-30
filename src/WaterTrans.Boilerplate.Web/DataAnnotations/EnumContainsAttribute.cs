using System;
using System.ComponentModel.DataAnnotations;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EnumContainsAttribute : AdapteredValidationAttribute
    {
        private readonly EnumDataTypeAttribute _enumDataType;

        public EnumContainsAttribute(Type enumType, params object[] enums)
        {
            _enumDataType = new EnumDataTypeAttribute(enumType);
            Enums = enums;
        }

        public object[] Enums { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (!_enumDataType.IsValid(value))
            {
                return false;
            }

            string stringValue = value as string;
            var convertedValue = stringValue != null
                        ? Enum.Parse(_enumDataType.EnumType, stringValue, false)
                        : Enum.ToObject(_enumDataType.EnumType, value);

            foreach (var item in Enums)
            {
                if (Enum.Equals(item, convertedValue))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
