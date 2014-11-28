using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;

namespace Wizardsgroup.Core.Web.ModuleRegistrator
{
    public interface IModuleItemContainer
    {
        List<MenuItem> Container { get; }         
    }
}
