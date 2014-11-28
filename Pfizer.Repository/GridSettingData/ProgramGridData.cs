using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class ProgramGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "ProgramViewModel",
            PrimaryKeyName = "ProgramId",
            GridName = "ProgramGrid"
        };

        public void CreateSchema(IContext context)
        {       
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("Name", 2)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkModal()
                                .CellBehaviour(true, true)
                                .CellProperties("Program", 186)
                                .ControllerProperties("Common", "Program", "Edit")
                                .ModalProperties("Edit Program", 550, 600).GetWrapperInstance());

                reg.For("Description", 3)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Description",296).GetWrapperInstance());

                reg.For("CardTypeName", 4)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Card Type",108).GetWrapperInstance());

                reg.For(_commonGridDataSchema.PrimaryKeyName, 5)
                    .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                        .CellBehaviour(false)
                        .CellProperties("Products", 100)
                        .CellLinkDetail(3)
                        .CellDisplayFormat(string.Empty, "View Details")
                        .ControllerProperties("Common", "ProgramProductMapping", "Index").GetWrapperInstance());

                reg.For(_commonGridDataSchema.PrimaryKeyName, 6)
                    .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                        .CellBehaviour(true,true)
                        .CellProperties("Prefix", 95)
                        .CellLinkDetail(3)
                        .CellDisplayFormat(string.Empty,"View Details")
                        .ControllerProperties("Common", "CardPrefix", "Index").GetWrapperInstance());

                reg.For("CardIdCount", 7)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Card IDs",101).GetWrapperInstance());

                reg.For("Status", 8)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Status").GetWrapperInstance());
            });
            }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
