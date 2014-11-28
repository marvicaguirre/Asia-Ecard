using Wizardsgroup.Core.Web.Helpers.ModuleProvider;

namespace Wizardsgroup.Core.Web.Helpers.MenuHelper
{
    public interface IMenuItem : IModuleItem
    {
        bool IsMenuSeparator { get; set; }
    }
}