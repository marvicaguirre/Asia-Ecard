using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Security
{
    internal class ModuleBasicFunctionRegistrator : IModuleBasicFunctionRegistrator
    {
        private readonly ModuleRegistrator _moduleRegistrator;
        private readonly IFunctionRegistrator _functionRegistrator;

        public ModuleBasicFunctionRegistrator(ModuleRegistrator moduleRegistrator)
        {
            moduleRegistrator.Guard("ModuleRegistrator must not be null.");
            _moduleRegistrator = moduleRegistrator;
            _functionRegistrator = new FunctionRegistrator(_moduleRegistrator);
        }

        public IModuleSpecialFunctionRegistrator WithBasicFunction(Action<IBasicFunctionRegistrator> specifyBasicFunction = null)
        {
            if (specifyBasicFunction == null)
            {
                specifyBasicFunction = AllFunctions();
            }

            return RegisterFunctions(specifyBasicFunction);
        }

        public IModuleSpecialFunctionRegistrator WithBasicFunction(Function regFunction)
        {            
            Action<IBasicFunctionRegistrator> function;

            if (regFunction == Function.ViewOnly)
            {
                function = registrator => registrator.Register(BasicFunction.View);
            }
            else
            {
                function = AllFunctions();            
            }
                
            return RegisterFunctions(function);
        }

        private Action<IBasicFunctionRegistrator> AllFunctions()
        {
            return registrator => registrator.Register(BasicFunction.Create)
                               .Register(BasicFunction.Delete)
                               .Register(BasicFunction.Edit)
                               .Register(BasicFunction.View)
                               .Register(BasicFunction.Toggle);
        }
        

        private IModuleSpecialFunctionRegistrator RegisterFunctions(Action<IFunctionRegistrator> functions)
        {
            functions(_functionRegistrator);
            IModuleSpecialFunctionRegistrator specialFunctionRegistrator = new ModuleSpecialFunctionRegistrator(_moduleRegistrator);
            return specialFunctionRegistrator;
        }
    }
}