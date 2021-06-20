using System;
using System.Globalization;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static string ToISO8601Date(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static string ToISO8601Date(this DateTime? value)
        {
            return value?.ToISO8601Date();
        }

        public static string ToISO8601(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
        }

        public static string ToISO8601(this DateTime? value)
        {
            return value?.ToISO8601();
        }
    }
}
