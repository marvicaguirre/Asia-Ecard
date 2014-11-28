using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class EmployeeGrid : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "EmployeeViewModel",
            PrimaryKeyName = "EmployeeId",
            GridName = "EmployeeGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateCheckbox()
                        .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("FullName", 2)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateLinkModal()
                        .CellBehaviour(true, true)
                        .CellProperties("Employee Name", 250)
                        .ControllerProperties("Common", "Employee", "Edit")
                        .ModalProperties("Edit Employee", 550, 630).GetWrapperInstance());

                reg.For("CompanyName", 3)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Company Name", 200).GetWrapperInstance());

                reg.For("DepartmentName", 4)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Department", 200).GetWrapperInstance());

                reg.For("TelephoneNo", 5)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Telephone Number", 150).GetWrapperInstance());

                reg.For("MobileNo", 6)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Mobile Number", 150).GetWrapperInstance());

                reg.For("Email", 7)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Email Address", 200).GetWrapperInstance());

                reg.For("EmployeeTypes", 8)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Employee Type", 150).GetWrapperInstance());

                reg.For("IsSupervisor", 9)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Supervisor", 100).GetWrapperInstance());
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
