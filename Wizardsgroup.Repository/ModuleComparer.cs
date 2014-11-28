using System.Collections.Generic;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Repository
{
    public class ModuleNameComparer : IEqualityComparer<SecurityFunctionInfo>
    {
        #region Implementation of IEqualityComparer<in CentralFunctionEx>

        public bool Equals(SecurityFunctionInfo x, SecurityFunctionInfo y)
        {
            return x.FullModuleName == y.FullModuleName;
        }

        public int GetHashCode(SecurityFunctionInfo obj)
        {
            return obj.FullModuleName.GetHashCode();
        }

        #endregion
    }
}
