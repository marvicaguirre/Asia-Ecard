using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Security
{
    internal class FunctionRegistrator : IFunctionRegistrator
    {
        private readonly ModuleRegistrator _moduleRegistrator;

        public FunctionRegistrator(ModuleRegistrator moduleRegistrator)
        {
            moduleRegistrator.Guard("ModuleRegistrator must not be null.");
            _moduleRegistrator = moduleRegistrator;
        }

        public ISpecialFunctionRegistrator Register(string specialFunction)
        {
            specialFunction.Guard("SpecialFunction must not be empty.");
            var result = GetModuleFunctionContainer();
            if (result.Functions.Any(o => o.ToLower() != specialFunction.ToLower()))
            {
                result.Functions.Add(specialFunction);
            }
            return this;
        }

        public IBasicFunctionRegistrator Register(BasicFunction basicFunction)
        {            
            var defaultWithViewFunction = GetDefaultFunctionsWithView();
            var result = GetModuleFunctionContainer();

            if (defaultWithViewFunction.Contains(basicFunction) && result.Functions.All(o => o != BasicFunction.View.ToString()))
            {
                result.Functions.Add(BasicFunction.View.ToString());
            }

            if (result.Functions.All(o => o.ToLower() != basicFunction.ToString().ToLower()))
            {
                switch (basicFunction)
                {
                    case BasicFunction.Delete:
                        result.Functions.Add("DeleteItems");
                        break;
                    case BasicFunction.Toggle:
                        result.Functions.Add("ToggleStatus");
                        break;
                    default:
                        result.Functions.Add(basicFunction.ToString());
                        break;                        
                }
                
            }
            return this;
        }

        private IModuleFunctionContainer GetModuleFunctionContainer()
        {
            var moduleContainer = _moduleRegistrator.ModuleFunctionContainer;
            var result = _moduleRegistrator.ModuleFunctionCollectionContainer.Container
                                           .Find(o => o.GroupName == moduleContainer.GroupName && o.ModuleName == moduleContainer.ModuleName);
            return result;
        }

        private List<BasicFunction> GetDefaultFunctionsWithView()
        {
            var defaultWithViewFunction = new List<BasicFunction>
                {
                    BasicFunction.Create,
                    BasicFunction.Delete,
                    BasicFunction.Edit,
                    BasicFunction.Toggle
                };
            return defaultWithViewFunction;
        }
    }
}