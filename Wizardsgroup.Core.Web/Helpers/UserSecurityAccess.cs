using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Utilities.Helpers;
using Wizardsgroup.Utilities.Security;

namespace Wizardsgroup.Core.Web.Helpers
{
    public sealed class UserSecurityAccess
    {
        public List<CentralFunctionEx> Containers { get; private set; }
        public List<IModuleFunctionContainer> ModuleFunctionContainers { get; private set; }

        public static UserSecurityAccess Instance
        {
            get { return Singleton<UserSecurityAccess>.Instance; }
        }

        public void SetSecurityAccessToUser(Func<int, List<CentralFunctionEx>> getSecurityOfUser, int userId)
        {
            Containers = getSecurityOfUser(userId);
        }

        public void SetModuleFunctionContainers(IReadOnlyCollection<IModuleFunctionContainer> containers)
        {
            ModuleFunctionContainers = containers.ToList();
        }
    }
}