using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Security
{
    internal class SubgroupModuleRegistrator : ISubgroupModuleRegistrator
    {        
        private readonly IModuleFunctionCollectionContainer _moduleFunctionCollectionContainer;
        private readonly string _groupModuleName;

        public SubgroupModuleRegistrator(IModuleFunctionCollectionContainer moduleFunctionCollectionContainer, string groupModuleName)
        {
            moduleFunctionCollectionContainer.Guard("IModuleFunctionCollectionContainer must not be null.");
            groupModuleName.Guard("GroupModuleName must not be null.");
            _moduleFunctionCollectionContainer = moduleFunctionCollectionContainer;
            _groupModuleName = groupModuleName;
        }

        public IModuleRegistrator IncludeSubgroup(string subgroupName, Action<IModuleFunctionRegistrator> regModule)
        {
            subgroupName.Guard("SubgroupName must not be null.");
            regModule.Guard("Action<IModuleFunctionRegistrator> must not be null.");
            IModuleRegistrator moduleRegistrator = new ModuleRegistrator(_moduleFunctionCollectionContainer, _groupModuleName, subgroupName);
            regModule(moduleRegistrator);
            return moduleRegistrator;
        }
    }
}