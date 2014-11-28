using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class CardTypeGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "CardTypeViewModel",
            PrimaryKeyName = "CardTypeId",
            GridName = "CardTypeGrid"
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
                                .CellProperties("CardType")
                                .ControllerProperties("Common", "CardType", "Edit")
                                .ModalProperties("Edit Card Type", 550, 400).GetWrapperInstance());

                reg.For("Description", 3)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Description").GetWrapperInstance());


                reg.For(_commonGridDataSchema.PrimaryKeyName, 4)
                    .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                        .CellBehaviour(false)
                        .CellProperties("Program",100)
                        .CellLinkDetail(3)
                        .CellDisplayFormat(string.Empty, "View Details")
                        .ControllerProperties("Common", "ProgramPerCardType", "Index").GetWrapperInstance());

                reg.For("Status", 5)
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
