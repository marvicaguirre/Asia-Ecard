using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class SalesRetailPriceGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "SalesRetailPriceViewModel",
            PrimaryKeyName = "SalesRetailPriceId",
            GridName = "SalesRetailPriceGrid"
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

                reg.For("From", 2)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("From").GetWrapperInstance());

                reg.For("To", 3)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("To").GetWrapperInstance());

                reg.For("PriceTypeName", 4)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Price Type").GetWrapperInstance());

                reg.For("Price", 5)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Price").GetWrapperInstance());

                reg.For("ConversionPerUnit", 6)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Conversion per Unit").GetWrapperInstance());
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
