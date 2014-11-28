using System;
using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    internal class MenuRegistrator : IMenuRegistrator
    {
        private readonly IMasterMenuRegistrator _masterMenuRegistrator;
        private readonly IReadOnlyCollection<IModuleFunctionContainer> _containers;        
        public MenuRegistrator(IMasterMenuRegistrator masterMenuRegistrator, IReadOnlyCollection<IModuleFunctionContainer> containers)
        {
            containers.Guard("IReadOnlyCollection<IModuleFunctionContainer> must not be bull.");
            masterMenuRegistrator.Guard("MasterMenuRegistrator must not be null.");
            _masterMenuRegistrator = masterMenuRegistrator;
            _containers = containers;
        }

        public IMenuRegistratorSeparator ForModuleFunction(string functionName, Action<IControllerRegistrator> register)
        {
            functionName.Guard("FunctionName must not be null or empty.");
            if (functionName.Equals("Separator"))
            {
                RegisterSeparator();
            }                
            else
            {
                register.Guard("Action<IControllerRegistrator> must not be null.");
                RegisterController(functionName, register);
            }
            
            return new MenuRegistratorSeparator(this);
        }

        private void RegisterSeparator()
        {
            _masterMenuRegistrator.Container.Add(SeparatorMenuHelper.CreateSeparator());
        }

        private void RegisterController(string functionName, Action<IControllerRegistrator> register)
        {
            var functionHelper = new FunctionHelper(_containers);
            var subGroupName = functionHelper.GetSecurityInfoFromFunctionName(functionName).SubGroupName;
            var moduleName = functionHelper.GetModuleFromFunctionName(functionName);
            var menuItem = new MenuItem
                {
                    ModuleName = moduleName,                    
                    MenuTitle = string.IsNullOrEmpty(subGroupName) ? string.Empty : subGroupName.SplitCamelCase(),
                    FunctionName = functionName,
                };
            _masterMenuRegistrator.Container.Add(menuItem);
            IControllerRegistrator controllerRegistrator = new ControllerRegistrator(_masterMenuRegistrator, menuItem, _containers);
            register(controllerRegistrator); 
        }
    }
}