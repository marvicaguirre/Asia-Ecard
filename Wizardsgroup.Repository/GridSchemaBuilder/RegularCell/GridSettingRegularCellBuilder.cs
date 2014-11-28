using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSettingRegularCellBuilder : AbstractSpecificGridColumnBuilder, IGridSettingRegularCellBuilder
    {
        public GridSettingRegularCellBuilder(IGridSettingDataBuilderWrapper gridSettingDataBuilderWrapper, IGridSettingDataBuilder gridSettingDataBuilder) : base(gridSettingDataBuilderWrapper, gridSettingDataBuilder)
        {
        }

        public IGridSettingRegularCellBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable,string gridColumnAlignment = "")
        {
            GridSettingDataBuilder.CellBehaviour(gridColumnFilterable, gridColumnSortable, gridColumnAlignment);
            return this;
        }

        public IGridSettingRegularCellBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "")
        {
            GridSettingDataBuilder.CellProperties(GridCellType.RegularCell, gridHeaderTitle, gridColumnWidth, gridColumnName);
            return this;
        }

        public IGridSettingRegularCellBuilder Lockable(bool lockable = true)
        {
            GridSettingDataBuilder.Lockable(lockable);
            return this;
        }

        public IGridSettingRegularCellBuilder Locked(bool locked = true)
        {
            GridSettingDataBuilder.Locked(locked);
            return this;
        }

        public IGridSettingRegularCellBuilder Groupable(bool groupable = true)
        {
            GridSettingDataBuilder.Groupable(groupable);
            return this;
        }
    }
}
