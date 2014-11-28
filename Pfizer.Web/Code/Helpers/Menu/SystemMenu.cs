using System;
using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;
using Wizardsgroup.Core.Web.ModuleRegistrator;
using Wizardsgroup.Utilities.Security;

namespace Pfizer.Web.Code.Helpers.Menu
{
    public class SystemMenu : AbstractMainMenu
    {
        public SystemMenu(IReadOnlyCollection<IModuleFunctionContainer> moduleFunctionContainers) : base(moduleFunctionContainers)
        {
        }

        public override string DisplayName
        {
            get { return "System"; }
        }

        public override string RegisteredSecurityGroupName
        {
            get { return "System"; }
        }

        protected override Action<IMenuRegistrator> RegisterMenutItems()
        {
            return registrator => registrator
                .ForModuleFunction("ViewDepartment", o => o
                    .ControllerProperties("Common", "Department", "Index")
                    .DisplayProperties("Department", "Department"))
                .ForModuleFunction("ViewEmployee", o => o
                    .ControllerProperties("Common", "Employee", "Index")
                    .DisplayProperties("Employee List", "Employee List"))
                .ForModuleFunction("ViewSystemSetting", o => o
                    .ControllerProperties("Common", "SystemSetting", "Index")
                    .DisplayProperties("System Setting", "System Setting"))
                .WithSeparator()
                .ForModuleFunction("ViewUserGroup", o => o
                    .ControllerProperties("Security", "UserGroup", "Index")
                    .DisplayProperties("User Groups and Functions", "User Groups and Functions"))
                .ForModuleFunction("ViewUser", o => o
                    .ControllerProperties("Security", "User", "Index")
                    .DisplayProperties("Users and Groups", "Users and Groups"))                
                .WithSeparator()

                .ForModuleFunction("Logoff", o => o
                    .ControllerProperties("Common", "Account", "Logoff")
                    .DisplayProperties("Logoff", "Logoff"));
        }
    }
}