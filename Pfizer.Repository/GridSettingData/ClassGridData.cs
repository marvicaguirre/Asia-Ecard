using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class ClassGridData : IBuildGridSchema
    {
        #region Member
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "ClassViewModel",
            PrimaryKeyName = "ClassId",
            GridName = "ClassGrid"
        };
        #endregion

        #region Functions
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
                                .CellProperties("Class")
                                .ControllerProperties("Common", "Class", "Edit")
                                .ModalProperties("Edit Class", 550, 400).GetWrapperInstance());

                reg.For("Description", 3)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Description").GetWrapperInstance());


                reg.For(_commonGridDataSchema.PrimaryKeyName, 4)
                    .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                        .CellBehaviour(false)
                        .CellProperties("Card Type")
                        .CellLinkDetail(0)
                        .CellDisplayFormat(string.Empty, "View Details")
                        .ControllerProperties("Common", "CardType", "Index").GetWrapperInstance());

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
        #endregion
    }
}
