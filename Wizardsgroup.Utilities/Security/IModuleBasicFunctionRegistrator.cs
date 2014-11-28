using System;

namespace Wizardsgroup.Utilities.Security
{
    public interface IModuleBasicFunctionRegistrator
    {
        IModuleSpecialFunctionRegistrator WithBasicFunction(Action<IBasicFunctionRegistrator> regBasicFunction = null);
        IModuleSpecialFunctionRegistrator WithBasicFunction(Function regFunction);
    }
}