using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class UserGroupMapGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Security.ViewModels",
            ModelName = "UserGroupMapViewModel",
            PrimaryKeyName = "UserGroupMapId",
            GridName = "UserGroupMapGrid"
        };

        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("UserGroupName", 2)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Group Name").GetWrapperInstance());

                reg.For("UserGroupDesc", 3)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Description").GetWrapperInstance());


            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
