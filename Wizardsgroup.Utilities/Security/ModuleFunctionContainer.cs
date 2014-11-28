using System.Collections.Generic;

namespace Wizardsgroup.Utilities.Security
{
    internal class ModuleFunctionContainer : IModuleFunctionContainer
    {
        public string GroupName { get; set; }
        public string SubgroupName { get; set; }
        public string ModuleName { get; set; }
        public List<string> Functions { get; set; }

        public ModuleFunctionContainer()
        {
            Functions = new List<string>();
        }
    }
}