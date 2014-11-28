namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingLinkDetailBuilder : IGridSettingBuilderWrapper
    {
        IGridSettingLinkDetailBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "");
        IGridSettingLinkDetailBuilder CellDisplayFormat(string gridCellDisplayFormat = "", string gridCellDisplayText = "");
        IGridSettingLinkDetailBuilder CellLinkDetail(int? level = null);
        IGridSettingLinkDetailBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false, string gridColumnAlignment = "");
        IGridSettingLinkDetailBuilder ControllerProperties(string area, string controllerName, string controllerAction);
        IGridSettingLinkDetailBuilder Lockable(bool lockable = true);
    }
}