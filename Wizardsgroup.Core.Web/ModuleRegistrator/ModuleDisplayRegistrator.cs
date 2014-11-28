using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    internal class ModuleDisplayRegistrator : IModuleDisplayRegistrator
    {
        private readonly IMasterMenuRegistrator _masterMenuRegistrator;
        private readonly MenuItem _menuItem;
        private readonly IReadOnlyCollection<IModuleFunctionContainer> _containers;

        public ModuleDisplayRegistrator(IMasterMenuRegistrator masterMenuRegistrator, MenuItem menuItem, IReadOnlyCollection<IModuleFunctionContainer> containers)
        {
            menuItem.Guard("MenuItem must not be null.");
            masterMenuRegistrator.Guard("MasterMenuRegistrator must not be null.");
            _masterMenuRegistrator = masterMenuRegistrator;
            _menuItem = menuItem;
            _containers = containers;
        }

        public IControllerRegistrator DisplayProperties(string moduleItemText, string tabCaption = "")
        {
            moduleItemText.Guard("moduleItemText must not be null or empty.");
            _menuItem.ModuleItemText = moduleItemText;
            _menuItem.TabCaption = string.IsNullOrEmpty(tabCaption) ? moduleItemText : tabCaption;
            return new ControllerRegistrator(_masterMenuRegistrator, _menuItem, _containers);
        }
    }
}