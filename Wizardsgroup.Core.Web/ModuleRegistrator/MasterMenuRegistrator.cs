using System;
using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public class MasterMenuRegistrator : IMasterMenuRegistrator
    {
        private readonly IReadOnlyCollection<IModuleFunctionContainer> _containers;
        public List<MenuItem> Container { get; private set; }
        public string ModuleName { get; private set; }        
        public MasterMenuRegistrator(IReadOnlyCollection<IModuleFunctionContainer> containers)
        {
            containers.Guard("IReadOnlyCollection<IModuleFunctionContainer> must not be null");
            _containers = containers;
            Container = new List<MenuItem>();
        }

        public void RegisterMenu(string mainMenu, Action<IMenuRegistrator> register)
        {
            mainMenu.Guard("mainMenu must not be empty or null");
            register.Guard("Action<IMenuRegistrator> must not be null.");
            ModuleName = mainMenu;
            IMenuRegistrator registrator = new MenuRegistrator(this,_containers);
            register(registrator);                      
        }
    }
}