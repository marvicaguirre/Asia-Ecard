using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class CardPrefixGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "CardPrefixViewModel",
            PrimaryKeyName = "CardPrefixId",
            GridName = "CardPrefixGrid"
        };
        public void CreateSchema(Wizardsgroup.Repository.IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                     .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("Name", 2)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateLinkModal()
                                .CellBehaviour(true, true)
                                .CellProperties("Card Type")
                                .ControllerProperties("Common", "CardPrefix", "Edit")
                                .ModalProperties("Edit Card Prefix", 550, 400).GetWrapperInstance());

                reg.For("Status", 3)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Status").GetWrapperInstance());

            });
        }

        public void DropSchema(Wizardsgroup.Repository.IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
