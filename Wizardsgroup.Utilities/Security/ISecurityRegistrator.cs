using System;

namespace Wizardsgroup.Utilities.Security
{
    public interface ISecurityRegistrator
    {        
        void Register(Action<IGroupModuleFunctionRegistrator> register);
    }
}
