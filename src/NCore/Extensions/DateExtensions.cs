using System;

namespace NCore.Extensions
{
    public static class DateExtensions
    {
        private static DateTime _1970_01_01 = new DateTime(1970, 1, 1);

        public static int DaysSince1970_01_01(this DateTime date)
        {
            return Convert.ToInt32(date.Subtract(_1970_01_01).TotalDays);
        }

        public static DateTime ToDate(this int daysSince1970_01_01)
        {
            return _1970_01_01.AddDays(daysSince1970_01_01);
        }

        public static DateTime? ToDate(this int? daysSince1970_01_01)
        {
            return daysSince1970_01_01?.ToDate();
        }

        public static string FriendlyString(this int? daysSince1970_01_01)
        {
            if (daysSince1970_01_01.HasValue)
                return daysSince1970_01_01.Value.FriendlyString();
            return string.Empty;
        }

        public static string FriendlyString(this int daysSince1970_01_01)
        {
            return daysSince1970_01_01.ToDate().ToShortDateString();
        }
    }
}