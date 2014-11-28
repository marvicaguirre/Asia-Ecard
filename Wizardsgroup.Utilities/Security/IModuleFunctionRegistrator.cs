using System;

namespace Wizardsgroup.Utilities.Security
{
    public interface IModuleFunctionRegistrator
    {
        IModuleFunctionRegistrator IncludeModule(string moduleName, Action<IModuleBasicFunctionRegistrator> regFunctions);
    }
}