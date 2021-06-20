using System;
using System.Globalization;

namespace WaterTrans.Boilerplate.CrossCuttingConcerns.Utils
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
    }
}
