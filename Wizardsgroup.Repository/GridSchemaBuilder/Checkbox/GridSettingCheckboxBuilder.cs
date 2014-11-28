using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    internal class GridSettingCheckboxBuilder : AbstractSpecificGridColumnBuilder, IGridSettingCheckboxBuilder
    {
        public GridSettingCheckboxBuilder(IGridSettingDataBuilderWrapper gridSettingDataBuilderWrapper, IGridSettingDataBuilder gridSettingDataBuilder) : base(gridSettingDataBuilderWrapper, gridSettingDataBuilder)
        {
        }

        public IGridSettingCheckboxBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null,string gridColumnName = "")
        {
            GridSettingDataBuilder.CellBehaviour(false);
            GridSettingDataBuilder.CellProperties(GridCellType.CheckBox, gridHeaderTitle, gridColumnWidth, gridColumnName);
            return this;
        }


    }
}
