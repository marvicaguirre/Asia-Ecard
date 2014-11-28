using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    public class UserGroupFunctionGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Security.ViewModels",
            ModelName = "UserGroupFunctionViewModel",
            PrimaryKeyName = "UserGroupFunctionId",
            GridName = "UserGroupFunctionGrid"
        };
        public void CreateSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateCheckbox()
                        .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("FunctionName", 2)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Function Name").GetWrapperInstance());

                reg.For("ModuleName", 3)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Module Name").GetWrapperInstance());
            });   
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}