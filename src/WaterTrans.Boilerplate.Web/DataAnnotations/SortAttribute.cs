using System;

namespace WaterTrans.Boilerplate.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false)]
    public class SortAttribute : AdapteredValidationAttribute
    {
        public SortAttribute(params string[] fields)
        {
            Fields = fields;
        }

        public string[] Fields { get; set; }

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

            foreach (string item in ((string)value).Split(','))
            {
                string comparison = item.Trim();
                if (comparison.StartsWith('-'))
                {
                    comparison = comparison.Substring(1).Trim();
                }

                bool found = false;
                foreach (string field in Fields)
                {
                    if (field.Equals(comparison, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
