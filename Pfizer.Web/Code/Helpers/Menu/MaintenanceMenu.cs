using System;
using System.Collections.Generic;
using Wizardsgroup.Core.Web.Helpers.MenuHelper;
using Wizardsgroup.Core.Web.ModuleRegistrator;
using Wizardsgroup.Utilities.Security;

namespace Pfizer.Web.Code.Helpers.Menu
{
    public class MaintenanceMenu : AbstractMainMenu
    {
        public MaintenanceMenu(IReadOnlyCollection<IModuleFunctionContainer> moduleFunctionContainers)
            : base(moduleFunctionContainers)
        {
        }

        public override string DisplayName
        {
            get { return "Maintenance"; }
        }

        public override string RegisteredSecurityGroupName
        {
            get { return "Maintenance"; }
        }

        protected override Action<IMenuRegistrator> RegisterMenutItems()
        {

            return registrator => registrator
                .ForModuleFunction("ViewClass", o => o
                    .ControllerProperties("Common", "Class", "Index")
                    .DisplayProperties("Class"))

                .ForModuleFunction("ViewProduct", o => o
                    .ControllerProperties("Common", "Product", "Index")
                    .DisplayProperties("Product"))

                .ForModuleFunction("ViewUnitOfMeasure", o => o
                    .ControllerProperties("Common", "UnitOfMeasure", "Index")
                    .DisplayProperties("Unit of Measure"))

                .ForModuleFunction("ViewProgram", o => o
                    .ControllerProperties("Common", "Program", "Index")
                    .DisplayProperties("Program"));

            //.ForModuleFunction("ViewCardType", o => o
            //    .ControllerProperties("Common", "CardType", "Index")
            //    .DisplayProperties("CardType"))

            //.ForModuleFunction("ViewDosage", o => o
            //    .ControllerProperties("Common", "Dosage", "Index")
            //    .DisplayProperties("Dosage"));
        }
    }
}