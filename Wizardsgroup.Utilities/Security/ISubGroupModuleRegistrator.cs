using System;

namespace Wizardsgroup.Utilities.Security
{
    public interface ISubgroupModuleRegistrator
    {
        IModuleRegistrator IncludeSubgroup(string subgroupName, Action<IModuleFunctionRegistrator> regModule);
    }
}