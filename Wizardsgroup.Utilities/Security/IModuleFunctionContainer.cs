using System.Collections.Generic;

namespace Wizardsgroup.Utilities.Security
{
    public interface IModuleFunctionContainer
    {
        string GroupName { get; set; }
        string SubgroupName { get; set; }
        string ModuleName { get; set; }
        List<string> Functions { get; set; }
    }
}