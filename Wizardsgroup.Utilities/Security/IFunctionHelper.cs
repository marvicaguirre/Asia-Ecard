using System.Collections.Generic;

namespace Wizardsgroup.Utilities.Security
{
    public interface IFunctionHelper
    {
        string GetModuleFromFunctionName(string functionName);
        IReadOnlyCollection<SecurityFunctionInfo> GetFunctionNames();
        SecurityFunctionInfo GetSecurityInfoFromFunctionName(string functionName);
    }
}