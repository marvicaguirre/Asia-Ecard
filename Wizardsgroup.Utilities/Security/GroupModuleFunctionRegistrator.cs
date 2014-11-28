using System.Collections.Generic;

namespace Wizardsgroup.Utilities.Security
{
    internal class GroupModuleFunctionRegistrator : IGroupModuleFunctionRegistrator, IModuleFunctionCollectionContainer
    {
        public List<IModuleFunctionContainer> Container { get; private set; }

        public GroupModuleFunctionRegistrator()
        {
            Container = new List<IModuleFunctionContainer>();
        }

        public IModuleRegistrator ForGroup(string groupModuleName)
        {        
            return new ModuleRegistrator(this,groupModuleName);
        }        
    }
}