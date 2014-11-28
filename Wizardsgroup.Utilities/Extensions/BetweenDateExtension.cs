using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class BetweenDateExtension
    {
        public static bool Between(this DateTime source,DateTime from,DateTime to)
        {
            return source >= from && source <= to;
        }
    }
}
