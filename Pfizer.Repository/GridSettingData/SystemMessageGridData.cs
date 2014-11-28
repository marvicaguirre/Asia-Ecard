using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;


namespace Pfizer.Repository.GridSettingData
{
  internal class SystemMessageGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "SystemMessageViewModel",
            PrimaryKeyName = "SystemMessageId",
            GridName = "SystemMessageGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For("Code", 1)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateLinkModal()
                        .CellBehaviour(true, true)
                        .CellProperties("Code")
                        .ControllerProperties("Common", "SystemMessage", "Edit")
                        .ModalProperties("Edit System Message", 400, 320).GetWrapperInstance());

                reg.For("Message", 2)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Message").GetWrapperInstance());
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }

    }
}
