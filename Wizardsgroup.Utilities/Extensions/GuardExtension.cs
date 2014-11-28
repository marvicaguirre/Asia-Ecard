using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class GuardExtension
    {
        public static void Guard(this object thisObjectToCheck,string message)
        {
            if (thisObjectToCheck == null)
            {
                throw new ArgumentNullException(message);
            }

            if (thisObjectToCheck.GetType().ToString().ToLower().Contains("string") && string.IsNullOrEmpty(thisObjectToCheck.ToString()))
            {
                throw new ArgumentNullException(message);
            }

        }     
    }
}
