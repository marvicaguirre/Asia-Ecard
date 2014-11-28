using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{

    internal class DataDictionaryGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "DataDictionaryViewModel",
            PrimaryKeyName = "DataDictionaryId",
            GridName = "DataDictionaryGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("Model", 2)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkModal()
                                .CellBehaviour(true, true)
                                .CellProperties("ViewModel Type")
                                .ControllerProperties("Common", "DataDictionary", "Edit")
                                .ModalProperties("Edit Data Dictionary", 300, 400)
                                .GetWrapperInstance());

                reg.For("FieldName", 3)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Field Name", 150).GetWrapperInstance());

                reg.For("FieldDisplayText", 4)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Field Display Text", 150).GetWrapperInstance());
                
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
