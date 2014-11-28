using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.Security
{
    public class SecurityModuleRegistrator : ISecurityRegistrator, IModuleFunctionCollectionContainer
    {
        private readonly GroupModuleFunctionRegistrator _groupModuleFunctionRegistrator = new GroupModuleFunctionRegistrator();
        public List<IModuleFunctionContainer> Container { get; private set; }

        public SecurityModuleRegistrator()
        {
            Container = new List<IModuleFunctionContainer>();
        }

        public void Register(Action<IGroupModuleFunctionRegistrator> register)
        {
            register.Guard("Action<IGroupModuleFunctionRegistrator> must not be null.");
            register(_groupModuleFunctionRegistrator);

            var newEntries = new List<IModuleFunctionContainer>();
            AddOrUpdateModuleFunctions(newEntries);
            Container.AddRange(newEntries);
        }

        private void AddOrUpdateModuleFunctions(List<IModuleFunctionContainer> newEntries)
        {
            _groupModuleFunctionRegistrator.Container.ForEach(registeredModule =>
                {
                    var existingModuleToUpdate = Container.Find(o => registeredModule.GroupName == o.GroupName && o.ModuleName == registeredModule.ModuleName);

                    if (existingModuleToUpdate == null)
                    {
                        newEntries.Add(registeredModule);
                    }
                    else
                    {
                        var newFunctions = registeredModule.Functions.Where(o=>!existingModuleToUpdate.Functions.Contains(o));
                        existingModuleToUpdate.Functions.AddRange(newFunctions);
                    }
                });
        }
    }
}