using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class UserGroupGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Security.ViewModels",
            ModelName = "UserGroupViewModel",
            PrimaryKeyName = "UserGroupId",
            GridName = "UserGroupViewGrid"
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
                                .CellProperties("Group Name")
                                .ControllerProperties("Security", "UserGroup", "Edit")
                                .ModalProperties("Edit User Group", 600, 500)
                                .GetWrapperInstance());

                reg.For("Description", 3)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Description").GetWrapperInstance());

                reg.For(_commonGridDataSchema.PrimaryKeyName, 4)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()                                
                                .CellBehaviour(false)
                                .CellDisplayFormat(string.Empty,"View Details")
                                .CellLinkDetail(1)
                                .ControllerProperties("Security","UserGroupFunction","Index")
                                .CellProperties("Functions").GetWrapperInstance());

                reg.For("Status", 5)
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
