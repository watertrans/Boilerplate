using System;
using System.Globalization;

namespace WaterTrans.Boilerplate.Domain.Utils
{
    public static class DateUtil
    {
        public static bool IsISO8601Date(string dateString)
        {
            return DateTime.TryParseExact((string)dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static DateTime ParseISO8601Date(string dateString)
        {
            return DateTime.ParseExact((string)dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static bool IsISO8601(string dateString)
        {
            return DateTime.TryParseExact((string)dateString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static DateTime ParseISO8601(string dateString)
        {
            return DateTime.ParseExact((string)dateString, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

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
