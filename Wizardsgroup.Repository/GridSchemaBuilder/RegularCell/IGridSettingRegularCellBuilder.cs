namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingRegularCellBuilder : IGridSettingBuilderWrapper
    {
        IGridSettingRegularCellBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false, string gridColumnAlignment = "");
        IGridSettingRegularCellBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "");
        IGridSettingRegularCellBuilder Lockable(bool lockable = true);
        IGridSettingRegularCellBuilder Locked(bool locked = true);
        IGridSettingRegularCellBuilder Groupable(bool groupable = true);
    }
}