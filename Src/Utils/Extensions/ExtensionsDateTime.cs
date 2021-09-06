using System;

namespace InteliSystem.Utils.Extensions
{
    public static class ExtensionsDateTime
    {
        public static bool ValidPeriodOfTime(this DateTime value, TimeSpan compare, int minutes)
        {
            var minutesAux = (compare - value.TimeOfDay);
            var comparar = TimeSpan.FromMinutes(minutes++);
            return (minutesAux < comparar);
        }

        public static bool ValidPeriodOfTime(this DateTime value, DateTime compare, int minutes)
        {
            if (value.AddMinutes(minutes).ToString("ddMMyyyy") != compare.AddMinutes(minutes).ToString("ddMMyyyy"))
                return false;
            var minutesAux = (compare.TimeOfDay - value.TimeOfDay);
            var comparar = TimeSpan.FromMinutes(minutes++);
            return (minutesAux < comparar);
        }


        public static string ToStringDateTimeBrazilian(this DateTime value)
        {
            return ToDateTime(value);
        }
        public static string ToStringDateBrazilian(this DateTime value)
        {
            return ToDate(value);
        }

        public static string ToStringDateBrazilian(this DateTime? value)
        {
            if (value == null)
                return "__/__/____";

            return ToDate((DateTime)value);
        }

        private static string ToDate(DateTime value)
        {
            var day = value.Day.ZeroLeft(2);
            var month = value.Month.ZeroLeft(2);
            var year = value.Year.ZeroLeft(4);
            return $"{day}/{month}/{year}";
        }

        private static string ToDateTime(DateTime value)
        {
            var data = ToDate(value);
            return $"{data} {value.ToString("HH:mm:ss")}";
        }
    }
}