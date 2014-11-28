using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class ProgramProductGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "ProgramProductMappingViewModel",
            PrimaryKeyName = "ProgramProductMappingId",
            GridName = "ProgramProductMappingGrid"
        };

        public void CreateSchema(IContext context)
        {       
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);

            register.Register(reg =>
            {
                reg.For(_commonGridDataSchema.PrimaryKeyName, 1)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateCheckbox()
                                .CellProperties(string.Empty, 30).GetWrapperInstance());

                reg.For("ProductName", 2)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Products").GetWrapperInstance());

                reg.For("Status", 3)
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
