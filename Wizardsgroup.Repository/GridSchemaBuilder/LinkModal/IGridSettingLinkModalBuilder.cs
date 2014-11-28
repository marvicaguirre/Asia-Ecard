namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingLinkModalBuilder : IGridSettingBuilderWrapper
    {
        IGridSettingLinkModalBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "");
        IGridSettingLinkModalBuilder ControllerProperties(string area, string controllerName, string controllerAction);
        IGridSettingLinkModalBuilder ModalProperties(string modalWindowTitle, int? modalWindowWidth = null, int? modalWindowHeight = null);
        IGridSettingLinkModalBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false, string gridColumnAlignment = "");
        IGridSettingLinkModalBuilder Groupable(bool groupable = true);
        IGridSettingLinkModalBuilder Lockable(bool lockable = true);
        IGridSettingLinkModalBuilder Locked(bool locked = false);
    }
}