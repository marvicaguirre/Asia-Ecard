using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Helpers.ModuleProvider
{
    public interface IModuleProvider
    {
        List<IModuleItem> GetModuleItems();
    }
}
