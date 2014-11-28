using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class DateExtension
    {
        public static double Age(this DateTime source)
        {
            TimeSpan span = DateTime.Today.Subtract(source);
            double age = Math.Round(span.TotalDays / 365.25, 2);
            return (age > 0) ? age : 0;
        }

        public static double Age(this DateTime? source)
        {
            if (source == null)
            {
                return 0;
            }
            TimeSpan span = DateTime.Today.Subtract((DateTime)source);
            double age = Math.Round(span.TotalDays / 365.25, 2);
            return (age > 0) ? age : 0;
        }
    }
}
