using Wizardsgroup.Core.Web.Helpers.ModuleProvider;

namespace Wizardsgroup.Core.Web.Helpers.MenuHelper
{
    public class MenuItem : ModuleItem, IMenuItem
    {
        public bool IsMenuSeparator { get; set; }
    }
}