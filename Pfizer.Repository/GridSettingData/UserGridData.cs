using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{

    internal class UserGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Security.ViewModels",
            ModelName = "UserViewModel",
            PrimaryKeyName = "UserId",
            GridName = "UserGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("UserName", 2)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkModal()
                                .CellBehaviour(true, true)
                                .CellProperties("User Name")
                                .ControllerProperties("Security", "User", "Edit")
                                .ModalProperties("Edit User", 500, 500).GetWrapperInstance());

                reg.For("FullName", 3)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Name").GetWrapperInstance());

                reg.For(_commonGridDataSchema.PrimaryKeyName, 4)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                                .CellBehaviour(false)
                                .CellProperties("User Group Map")
                                .CellLinkDetail(1)
                                .CellDisplayFormat(string.Empty, "View Detail")
                                .ControllerProperties("Security", "UserGroupMap", "Index").GetWrapperInstance());

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
