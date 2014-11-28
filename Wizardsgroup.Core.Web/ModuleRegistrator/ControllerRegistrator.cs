using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    internal class ControllerRegistrator : IControllerRegistrator
    {
        private readonly IMasterMenuRegistrator _masterMenuRegistrator;
        private readonly MenuItem _menuItem;
        private readonly IReadOnlyCollection<IModuleFunctionContainer> _containers;

        public ControllerRegistrator(IMasterMenuRegistrator masterMenuRegistrator, MenuItem menuItem, IReadOnlyCollection<IModuleFunctionContainer> containers)
        {
            menuItem.Guard("MenuItem must not be null.");
            masterMenuRegistrator.Guard("MasterMenuRegistrator must not be null.");
            _masterMenuRegistrator = masterMenuRegistrator;
            _menuItem = menuItem;
            _containers = containers;
        }

        public IModuleDisplayRegistrator ControllerProperties(string controllerArea, string controllerName, string controllerAction)
        {
            controllerArea.Guard("controllerArea must not be null.");
            controllerName.Guard("controllerName must not be null.");
            controllerAction.Guard("controllerAction must not be null.");
            _menuItem.ControllerArea = controllerArea;
            _menuItem.ControllerName = controllerName;
            _menuItem.ControllerAction = controllerAction;
            return new ModuleDisplayRegistrator(_masterMenuRegistrator, _menuItem, _containers);
        }
    }
}