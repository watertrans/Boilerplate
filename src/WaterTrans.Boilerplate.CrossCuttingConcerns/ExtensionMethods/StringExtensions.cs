using System;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Left(this string value, int length)
        {
            length = Math.Abs(length);
            return string.IsNullOrEmpty(value) ? value : value.Substring(0, Math.Min(value.Length, length));
        }

        public static string Right(this string value, int length)
        {
            length = Math.Abs(length);
            return string.IsNullOrEmpty(value) ? value : value.Substring(value.Length - Math.Min(value.Length, length));
        }

        public static bool EqualsIgnoreCase(this string source, string toCheck)
        {
            return string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static string ToCamelCase(this string value)
        {
            if (value == null || value.Length == 0)
            {
                return value;
            }
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
