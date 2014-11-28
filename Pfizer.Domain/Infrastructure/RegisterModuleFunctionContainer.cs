using System.Collections.Generic;
using Wizardsgroup.Utilities.Helpers;
using Wizardsgroup.Utilities.Security;

namespace Pfizer.Domain.Infrastructure
{
    public sealed class RegisterModuleFunctionContainer
    {
        public IReadOnlyCollection<IModuleFunctionContainer> Container { get; private set; }        
        
        public static RegisterModuleFunctionContainer Instance
        {
            get { return Singleton<RegisterModuleFunctionContainer>.Instance; }
        }

        private RegisterModuleFunctionContainer()
        {            
            var registrators = GetRegistrators();
            var securityRegister = new SecurityModuleRegistrator();
            registrators.ForEach(o=>o.Register(securityRegister));
            Container = securityRegister.Container;            
        }

        private static List<IRegistrator> GetRegistrators()
        {
            return new List<IRegistrator>
                {                 
                   new System(),
                    new MaintenanceRegistrator()
                };
        }
    }
}
