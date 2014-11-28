using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Core.Web.Helpers.HomePageHelper;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;

namespace Wizardsgroup.Core.Web.Helpers.ModuleProvider
{
    public static class ModuleItemConverter
    {
        public static List<MenuItem> ConvertToMenuItems(List<IModuleItem> moduleItems)
        {
            return moduleItems.Select(moduleItem => new MenuItem
            {
                ControllerAction = moduleItem.ControllerAction
                ,
                ControllerArea = moduleItem.ControllerArea
                ,
                ControllerName = moduleItem.ControllerName
                ,
                FunctionName = moduleItem.FunctionName
                ,
                ModuleItemText = moduleItem.ModuleItemText
                ,
                ModuleName = moduleItem.ModuleName
                ,
                TabCaption = moduleItem.TabCaption
                ,
                IsMenuSeparator = false
                ,
                ParameterKey = moduleItem.ParameterKey
                ,
                MenuTitle = moduleItem.MenuTitle
            }).ToList();
        }

        public static List<HomePageItem> ConvertToHomePageItems(List<IModuleItem> moduleItems)
        {
            return moduleItems.Select(moduleItem => new HomePageItem
            {
                ControllerAction = moduleItem.ControllerAction
                ,
                ControllerArea = moduleItem.ControllerArea
                ,
                ControllerName = moduleItem.ControllerName
                ,
                FunctionName = moduleItem.FunctionName
                ,
                ModuleItemText = moduleItem.ModuleItemText
                ,
                ModuleName = moduleItem.ModuleName
                ,
                TabCaption = moduleItem.TabCaption
                ,
                ParameterKey = moduleItem.ParameterKey
                ,
                MenuTitle = moduleItem.MenuTitle
            }).ToList();
        }
    }
}