using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizardsgroup.Repository.GridSchemaBuilder;

namespace Pfizer.Repository.GridSettingData
{
    internal class UnitOfMeasureGridData : IBuildGridSchema
    {
        readonly CommonGridDataSchema _commonGridDataSchema = new CommonGridDataSchema
        {
            ModelNamespace = "Pfizer.UI.Areas.Common.ViewModels",
            ModelName = "UnitOfMeasureViewModel",
            PrimaryKeyName = "UnitOfMeasureId",
            GridName = "UnitOfMeasureGrid"
        };
        public void CreateSchema(Wizardsgroup.Repository.IContext context)
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
                                .CellProperties("Unit Of Measure")
                                .ControllerProperties("Common", "UnitOfMeasure", "Edit")
                                .ModalProperties("Edit Unit of Measure", 550, 400).GetWrapperInstance());

                reg.For("Description", 3)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Description").GetWrapperInstance());

                reg.For("Status", 4)
                   .Use(() => new GridSettingDataBuilderWrapper()
                                .CreateRegularCell()
                                .CellBehaviour(true, true)
                                .CellProperties("Status").GetWrapperInstance());

            });
        }

        public void DropSchema(Wizardsgroup.Repository.IContext context)
        {
            var register = new GridSchemaCollectionRegistrator(context, _commonGridDataSchema);
            register.Unregister();
        }
    }
}
