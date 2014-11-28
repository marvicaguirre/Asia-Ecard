using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class RuleDatastoreGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "RuleDatastoreViewModel",
            PrimaryKeyName = "RuleDatastoreId",
            GridName = "RuleDatastoreGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("Controller", 2)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkModal()
                                .CellBehaviour(true, true)
                                .CellProperties("Controller")
                                .ControllerProperties("Common", "RuleDatastore", "Edit")
                                .ModalProperties("Edit Rule", 300, 600).GetWrapperInstance());

                reg.For("ControllerAction", 3)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("ControllerAction").GetWrapperInstance());

                reg.For("Field", 4)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Field").GetWrapperInstance());

                reg.For("RuleOperator", 5)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("RuleOperator").GetWrapperInstance());

                reg.For("Value", 6)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Value").GetWrapperInstance());

                reg.For("ValidationMessage", 7)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("ValidationMessage").GetWrapperInstance());
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
