using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    internal class MenuRegistratorSeparator : IMenuRegistratorSeparator
    {
        private readonly IMenuRegistrator _menuRegistrator;

        public MenuRegistratorSeparator(IMenuRegistrator menuRegistrator)
        {
            menuRegistrator.Guard("MenuRegistrator must not be null.");
            _menuRegistrator = menuRegistrator;
        }

        public IMenuRegistratorSeparator ForModuleFunction(string functionName, Action<IControllerRegistrator> register)
        {
            _menuRegistrator.ForModuleFunction(functionName, register);
            return this;
        }

        public IMenuRegistrator WithSeparator()
        {
            _menuRegistrator.ForModuleFunction("Separator", null);
            return _menuRegistrator;
        }
    }
}