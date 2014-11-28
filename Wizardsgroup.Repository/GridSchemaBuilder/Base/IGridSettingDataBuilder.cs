using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public interface IGridSettingDataBuilder
    {
        IGridSettingDataBuilder EntityType(string nameSpace, string dataSourceEntityType);
        IGridSettingDataBuilder EntityProperties(string dataSourceEntityKeyColumnName, string dataSourceEntityColumnName);
        IGridSettingDataBuilder CellProperties(GridCellType gridCellType, string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "");
        IGridSettingDataBuilder CellDisplayFormat(string gridCellDisplayFormat = "", string gridCellDisplayText = "");
        IGridSettingDataBuilder CellLinkDetail(int? level = null);
        IGridSettingDataBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false, string gridColumnAlignment = "");
        IGridSettingDataBuilder ControllerProperties(string area, string controllerName, string controllerAction);
        IGridSettingDataBuilder ModalProperties(string modalWindowTitle, int? modalWindowWidth = null, int? modalWindowHeight = null);
        IGridSettingDataBuilder SortOrder(int sortOrder);
        IGridSettingDataBuilder GridProperty(string gridName);
        IGridSettingDataBuilder Lockable(bool lockable = true);
        IGridSettingDataBuilder Locked(bool locked = true);
        IGridSettingDataBuilder Groupable(bool groupable = true);
        GridSetting GetGridInstance();
        bool OverrideKey { get; set; }
    }  
}