using System;

namespace Wizardsgroup.Utilities.Security
{
    public interface IModuleRegistrator : IModuleFunctionRegistrator, ISubgroupModuleRegistrator
    {
        new IModuleRegistrator IncludeModule(string moduleName, Action<IModuleBasicFunctionRegistrator> regFunctions);
    }
}