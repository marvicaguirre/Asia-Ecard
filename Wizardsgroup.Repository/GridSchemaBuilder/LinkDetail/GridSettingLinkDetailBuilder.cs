using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSettingLinkDetailBuilder : AbstractSpecificGridColumnBuilder, IGridSettingLinkDetailBuilder
    {
        public GridSettingLinkDetailBuilder(IGridSettingDataBuilderWrapper gridSettingDataBuilderWrapper, IGridSettingDataBuilder gridSettingDataBuilder) : base(gridSettingDataBuilderWrapper, gridSettingDataBuilder)
        {
            GridSettingDataBuilder.OverrideKey = gridSettingDataBuilderWrapper.OverrideKey;
        }

        public IGridSettingLinkDetailBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null,string gridColumnName = "")
        {
            GridSettingDataBuilder.CellProperties(GridCellType.LinkDetails, gridHeaderTitle, gridColumnWidth, gridColumnName);
            return this;
        }

        public IGridSettingLinkDetailBuilder CellDisplayFormat(string gridCellDisplayFormat = "", string gridCellDisplayText = "")
        {
            GridSettingDataBuilder.CellDisplayFormat(gridCellDisplayFormat, gridCellDisplayText);
            return this;
        }

        public IGridSettingLinkDetailBuilder CellLinkDetail(int? level = null)
        {
            GridSettingDataBuilder.CellLinkDetail(level);
            return this;
        }

        public IGridSettingLinkDetailBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false,string gridColumnAlignment = "")
        {
            GridSettingDataBuilder.CellBehaviour(gridColumnFilterable, gridColumnSortable, gridColumnAlignment);
            return this;
        }

        public IGridSettingLinkDetailBuilder ControllerProperties(string area, string controllerName, string controllerAction)
        {
            GridSettingDataBuilder.ControllerProperties(area, controllerName, controllerAction);
            return this;
        }

        public IGridSettingLinkDetailBuilder Lockable(bool lockable = true)
        {
            GridSettingDataBuilder.Lockable(lockable);
            return this;
        }
    }
}
