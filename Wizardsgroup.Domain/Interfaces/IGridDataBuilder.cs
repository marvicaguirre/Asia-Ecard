using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Domain.Interfaces
{
    public interface IGridDataBuilder
    {
        IGridDataBuilder EntityType(string nameSpace, string dataSourceEntityType);
        IGridDataBuilder EntityProperties(string dataSourceEntityKeyColumnName,string dataSourceEntityColumnName);
        IGridDataBuilder CellProperties(GridCellType gridCellType, string gridHeaderTitle,int? gridColumnWidth = null,string gridColumnName = "");
        IGridDataBuilder CellDisplayFormat(string gridCellDisplayFormat="",string gridCellDisplayText="");
        IGridDataBuilder CellLinkDetail(int? level = null);
        IGridDataBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false,string gridColumnAlignment = "");
        IGridDataBuilder ContollerProperties(string area,string controllerName,string controllerAction);
        IGridDataBuilder ModalProperties(string modalWindowTitle, int? modalWindowWidth = null, int? modalWindowHeight = null);
        IGridDataBuilder SortOrder(int sortOrder);
        GridSetting Build(string gridName);
    }

}