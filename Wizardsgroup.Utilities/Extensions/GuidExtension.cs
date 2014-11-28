using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class GuidExtension
    {
        public static bool IsNullOrEmpty(this Guid source)
        {
            return (source == null || source == Guid.Empty) ? true : false;
        }

        public static bool IsNullOrEmpty(this Guid? source)
        {
            return (source == null || source == Guid.Empty) ? true : false;
        }

        public static Guid SetEmptyIfNull(this Guid? source)
        {
            return (source == null) ? Guid.Empty : (Guid)source;
        }

        public static Guid? SetToNullIfDefault(this Guid? source)
        {
            return (source == new Guid("11111111-1111-1111-1111-111111111111")) ? null : source;
        }
    }
}