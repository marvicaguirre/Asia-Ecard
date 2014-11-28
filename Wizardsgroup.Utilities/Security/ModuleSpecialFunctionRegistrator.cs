using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Security
{
    internal class ModuleSpecialFunctionRegistrator : IModuleSpecialFunctionRegistrator
    {
        private readonly ModuleRegistrator _moduleRegistrator;
        private readonly IFunctionRegistrator _functionRegistrator;

        public ModuleSpecialFunctionRegistrator(ModuleRegistrator moduleRegistrator)
        {
            moduleRegistrator.Guard("ModuleRegistrator must not be null.");
            _moduleRegistrator = moduleRegistrator;
            _functionRegistrator = new FunctionRegistrator(_moduleRegistrator);
        }

        public IModuleBasicFunctionRegistrator WithSpecialFunction(Action<ISpecialFunctionRegistrator> regSpecialFunction)
        {
            regSpecialFunction.Guard("Action<ISpecialFunctionRegistrator> must not be null.");
            regSpecialFunction(_functionRegistrator);                        
            return new ModuleBasicFunctionRegistrator(_moduleRegistrator);
        }
    }
}