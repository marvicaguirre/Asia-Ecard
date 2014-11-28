using System;
using System.Collections.Generic;
using Wizardsgroup.Core.Web.ModuleRegistrator;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.Helpers.MenuHelper
{
    public abstract class AbstractMainMenu : IMenuHelper
    {        
        private readonly IMasterMenuRegistrator _masterMenuRegistrator;

        protected AbstractMainMenu(IReadOnlyCollection<IModuleFunctionContainer> moduleFunctionContainers)
        {
            moduleFunctionContainers.Guard("IReadOnlyCollection<IModuleFunctionContainer> must not be null.");

            if (moduleFunctionContainers.Count == 0) 
                throw new ArgumentException("IReadOnlyCollection<IModuleFunctionContainer> must be more than zero","moduleFunctionContainers");

            _masterMenuRegistrator = new MasterMenuRegistrator(moduleFunctionContainers);
        }

        public List<MenuItem> GetMenuItems()
        {
            _masterMenuRegistrator.RegisterMenu(RegisteredSecurityGroupName, RegisterMenutItems());
            return _masterMenuRegistrator.Container;
        }

        public abstract string DisplayName { get; }
        public abstract string RegisteredSecurityGroupName { get; }
        protected abstract Action<IMenuRegistrator> RegisterMenutItems();
    }
}