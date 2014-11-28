using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    class DosageGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "DosageViewModel",
            PrimaryKeyName = "DosageId",
            GridName = "DosageGrid"
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
                        .CellProperties("Dosage Form",250)
                        .ControllerProperties("Common", "Dosage", "Edit")
                        .ModalProperties("Edit Dosage", 550, 400).GetWrapperInstance());

                reg.For("NoOfCoversionFactor", 3)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                       .CellBehaviour(false)
                       .CellProperties("Pfizer Code & Conversion Factor",100)
                       .CellLinkDetail(2)
                       //TODO: Change "View Details" static title to the total count of Pfizer code assigned to the dosage form
                       .CellDisplayFormat(string.Empty, "View Details")
                       .ControllerProperties("Common", "ConversionFactor", "Index").GetWrapperInstance());

                reg.For(_commonGridDataSchema.PrimaryKeyName, 4)
                   .Use(() => new GridSettingDataBuilderWrapper().CreateLinkDetail()
                       .CellBehaviour(false)
                       .CellProperties("Price")
                       .CellLinkDetail(0).CellDisplayFormat(string.Empty, "View Details")
                       .ControllerProperties("Common", "SalesRetailPrice", "Index").GetWrapperInstance());

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
