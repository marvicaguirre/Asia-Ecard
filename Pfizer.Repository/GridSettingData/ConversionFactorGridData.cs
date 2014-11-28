using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    public class ConversionFactorGridData : IBuildGridSchema
    {
        
    
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "ConversionFactorViewModel",
            PrimaryKeyName = "ConversionFactorId",
            GridName = "ConversionFactorGrid"
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

                reg.For("PfizerCode", 2)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateLinkModal()
                                .CellBehaviour(true, true)
                                .CellProperties("Pfizer Code")
                                .ControllerProperties("Common", "ConversionFactor", "Edit")
                                .ModalProperties("Edit Conversion Factor", 550, 400).GetWrapperInstance());

                reg.For("Description", 3)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Unit of Measure").GetWrapperInstance());

                reg.For("Factor", 4)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Conversion Factor").GetWrapperInstance());
            });
        }

        public void DropSchema(IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
