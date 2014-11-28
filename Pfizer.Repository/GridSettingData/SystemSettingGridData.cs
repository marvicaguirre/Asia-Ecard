using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;


namespace Pfizer.Repository.GridSettingData
{
  internal class SystemSettingGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "SystemSettingViewModel",
            PrimaryKeyName = "SystemSettingId",
            GridName = "SystemSettingGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For("SettingName", 1)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateLinkModal()
                        .CellBehaviour(true, true)
                        .CellProperties("Name")
                        .ControllerProperties("Common", "SystemSetting", "Edit")
                        .ModalProperties("Edit System Setting", 400, 320).GetWrapperInstance());

                reg.For("SettingValue", 2)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Value").GetWrapperInstance());

                reg.For("DataType", 3)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Type").GetWrapperInstance());
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }

    }
}
