
namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingActionLinkBuilder : IGridSettingBuilderWrapper
    {
        IGridSettingActionLinkBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "");
        IGridSettingActionLinkBuilder ControllerProperties(string area, string controllerName, string controllerAction);
        IGridSettingActionLinkBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false, string gridColumnAlignment = "");
        IGridSettingActionLinkBuilder CellDisplayFormat(string gridCellDisplayFormat = "", string gridCellDisplayText = "");
        IGridSettingActionLinkBuilder Lockable(bool lockable = true);
    }
}
