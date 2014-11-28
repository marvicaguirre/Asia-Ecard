using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    public class ProductGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "ProductViewModel",
            PrimaryKeyName = "ProductId",
            GridName = "ProductGrid"
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

                reg.For("Name", 2)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateLinkModal()
                        .CellBehaviour(true, true)
                        .CellProperties("Product")
                        .ControllerProperties("Common", "Product", "Edit")
                        .ModalProperties("Edit Product", 550, 400).GetWrapperInstance());

                reg.For("Description", 3)
                    .Use(() => new GridSettingDataBuilderWrapper()
                        .CreateRegularCell()
                        .CellBehaviour(true, true)
                        .CellProperties("Description").GetWrapperInstance());

                reg.For(_commonGridDataSchema.PrimaryKeyName, 4)
                    .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                        .CellBehaviour(false)
                        .CellProperties("Dosage Form")
                        .CellLinkDetail(2)
                        .CellDisplayFormat(string.Empty, "View Details")
                        .ControllerProperties("Common", "Dosage", "Index").GetWrapperInstance());

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
