using System;

namespace SampleServer.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsTimeInRange(this DateTime time, TimeSpan from, TimeSpan to)
        {
            if (to < from)
            {
                return !(to < time.TimeOfDay && time.TimeOfDay < from);
            }
            return from <= time.TimeOfDay && time.TimeOfDay <= to;
        }

        public static TimeSpan GetTimeToRangeBorder(this DateTime time, TimeSpan from, TimeSpan to)
        {
            if (time.IsTimeInRange(from, to))
            {
                return TimeSpan.Zero;
            }

            if (to < from)
            {
                return from - time.TimeOfDay;
            }

            if (time.TimeOfDay < from)
            {
                return from - time.TimeOfDay;
            }

            return TimeSpan.FromHours(24) - time.TimeOfDay + from;
        }
    }
}