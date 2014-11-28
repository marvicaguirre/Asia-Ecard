using Wizardsgroup.Domain.Enumerations;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSettingLinkModalBuilder : AbstractSpecificGridColumnBuilder, IGridSettingLinkModalBuilder
    {
        public GridSettingLinkModalBuilder(IGridSettingDataBuilderWrapper gridSettingDataBuilderWrapper, IGridSettingDataBuilder gridSettingDataBuilder) : base(gridSettingDataBuilderWrapper, gridSettingDataBuilder)
        {
        }

        public IGridSettingLinkModalBuilder CellProperties(string gridHeaderTitle, int? gridColumnWidth = null, string gridColumnName = "")
        {
            GridSettingDataBuilder.CellProperties(GridCellType.LinkModal, gridHeaderTitle, gridColumnWidth, gridColumnName);
            return this;
        }

        public IGridSettingLinkModalBuilder ControllerProperties(string area, string controllerName, string controllerAction)
        {
            GridSettingDataBuilder.ControllerProperties(area, controllerName, controllerAction);
            return this;
        }

        public IGridSettingLinkModalBuilder ModalProperties(string modalWindowTitle, int? modalWindowWidth = null,int? modalWindowHeight = null)
        {
            GridSettingDataBuilder.ModalProperties(modalWindowTitle, modalWindowWidth, modalWindowHeight);
            return this;
        }

        public IGridSettingLinkModalBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable,string gridColumnAlignment = "")
        {
            GridSettingDataBuilder.CellBehaviour(gridColumnFilterable, gridColumnSortable, gridColumnAlignment);
            return this;
        }

        public IGridSettingLinkModalBuilder Groupable(bool groupable = true)
        {
            GridSettingDataBuilder.Groupable(groupable);
            return this;
        }

        public IGridSettingLinkModalBuilder Lockable(bool lockable = true)
        {
            GridSettingDataBuilder.Lockable(lockable);
            return this;
        }

        public IGridSettingLinkModalBuilder Locked(bool locked = false)
        {
            GridSettingDataBuilder.Locked(locked);
            return this;
        }
    }
}
