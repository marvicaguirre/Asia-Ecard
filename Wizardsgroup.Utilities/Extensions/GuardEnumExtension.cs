namespace Wizardsgroup.Utilities.Extensions
{
    public static class GuardEnumExtension
    {
        private static object _enumGuard;
        public static IEnumGuard<T> FluentEnumGuard<T>(this T enumToCheck)
        {            
            _enumGuard = new EnumGuard<T>(enumToCheck);
            return _enumGuard as EnumGuard<T>;
        }        
    }
}