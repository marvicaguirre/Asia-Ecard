using System;

namespace Wizardsgroup.Utilities.Extensions
{
    public static class ConvertTo
    {

        public static bool IsConvertibleToGuid(this object param)
        {
            Guid result;
            //return Guid.TryParse((string)(param is string ? param : param.ToString()), out result);  
            return Guid.TryParse(param.ToString(), out result);   
        }

        public static Guid ToGuid(this object param)
        {
            if (param == null)
                return default(Guid);

            Guid result;
            //Guid.TryParse((string)(param is string ? param : param.ToString()), out result);
            Guid.TryParse(param.ToString(), out result);
            return result;
        }

        public static bool IsConvertibleToInteger(this object param)
        {
            int result;
            //return int.TryParse((string)(param is string ? param : param.ToString()), out result);     
            return int.TryParse(param.ToString(), out result);   
        }

        public static int ToInteger(this object param)
        {
            if (param == null)
                return default(int);

            int result;
            //int.TryParse((string) (param is string ? param : param.ToString()), out result);
            int.TryParse(param.ToString(), out result);
            return result;
        }
    }
}
