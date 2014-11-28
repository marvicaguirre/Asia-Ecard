using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Security
{
    internal class ModuleRegistrator : IModuleRegistrator
    {
        internal readonly IModuleFunctionCollectionContainer ModuleFunctionCollectionContainer;
        private readonly string _groupModuleName;
        private readonly string _subgroupName;
        internal ModuleFunctionContainer ModuleFunctionContainer { get; private set; }

        internal ModuleRegistrator(IModuleFunctionCollectionContainer moduleFunctionCollectionContainer, string groupModuleName,string subgroupName = "")
        {
            moduleFunctionCollectionContainer.Guard("Container must not be null.");
            groupModuleName.Guard("GroupModuleName must not be empty.");
            ModuleFunctionCollectionContainer = moduleFunctionCollectionContainer;
            _groupModuleName = groupModuleName;
            _subgroupName = subgroupName;
        }

        public IModuleRegistrator IncludeModule(string moduleName, Action<IModuleBasicFunctionRegistrator> regFunctions)
        {
            regFunctions.Guard("Function registration must not be null.");
            moduleName.Guard("ModuleName must not be null.");
            CreateNewModuleFunctionContainer(moduleName);
            ModuleFunctionCollectionContainer.Container.Add(ModuleFunctionContainer);
            IModuleBasicFunctionRegistrator basicFunctionRegistrator = new ModuleBasicFunctionRegistrator(this);
            regFunctions(basicFunctionRegistrator);
            return this;
        }

        IModuleFunctionRegistrator IModuleFunctionRegistrator.IncludeModule(string moduleName, Action<IModuleBasicFunctionRegistrator> regFunctions)
        {
            return IncludeModule(moduleName, regFunctions);
        }

        public IModuleRegistrator IncludeSubgroup(string subgroupName, Action<IModuleFunctionRegistrator> regModule)
        {
            ISubgroupModuleRegistrator moduleRegistrator = new SubgroupModuleRegistrator(ModuleFunctionCollectionContainer, _groupModuleName);
            moduleRegistrator.IncludeSubgroup(subgroupName, regModule);
            return this;
        }

        private void CreateNewModuleFunctionContainer(string moduleName)
        {
            ModuleFunctionContainer = new ModuleFunctionContainer
            {
                GroupName = _groupModuleName,
                ModuleName = moduleName,
                SubgroupName = _subgroupName
            };
        }
    }
}