using System;

namespace Wizardsgroup.Utilities.Security
{
    public interface IModuleSpecialFunctionRegistrator
    {
        IModuleBasicFunctionRegistrator WithSpecialFunction(Action<ISpecialFunctionRegistrator> regSpecialFunction);
    }
}