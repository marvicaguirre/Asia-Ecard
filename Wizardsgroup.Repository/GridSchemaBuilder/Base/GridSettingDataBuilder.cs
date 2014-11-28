using System;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Domain.Models;
using Wizardsgroup.Repository.GridSchemaBuilder.Base.WidthHandler;

namespace Wizardsgroup.Repository.GridSchemaBuilder
{
    public class GridSettingDataBuilder : IGridSettingDataBuilder
    {
        protected readonly GridSetting GridSetting = new GridSetting();

        public IGridSettingDataBuilder EntityType(string nameSpace, string dataSourceEntityType)
        {
            var fullyQualifiedType = string.Format("{0},{0}.{1}", nameSpace, dataSourceEntityType);
            GridSetting.EntityPropertyName = fullyQualifiedType;
            GridSetting.DataSourceEntityType = dataSourceEntityType;
            return this;
        }

        public IGridSettingDataBuilder EntityProperties(string dataSourceEntityKeyColumnName, string dataSourceEntityColumnName)
        {
            GridSetting.DataSourceEntityKeyColumnName = dataSourceEntityKeyColumnName;
            GridSetting.DataSourceEntityColumnName = dataSourceEntityColumnName;
            return this;
        }

        public IGridSettingDataBuilder CellProperties(GridCellType gridCellType, string gridHeaderTitle = "", int? gridColumnWidth = null,
                                               string gridColumnName = "")
        {
            GridSetting.GridCellType = gridCellType.ToString();            
            GridSetting.GridColumnMenu = gridCellType != GridCellType.CheckBox;
            GridSetting.GridColumnName = gridColumnName;
            GridSetting.GridColumnLocked = gridCellType == GridCellType.CheckBox || gridCellType == GridCellType.LinkModal;
            GridSetting.GridColumnLockable = gridCellType == GridCellType.CheckBox ? true : (bool?)null;
            GridSetting.GridColumnGroupable = !(gridCellType == GridCellType.CheckBox || gridCellType == GridCellType.LinkDetails);

            Func<bool> isCheckBoxCell = () => gridCellType == GridCellType.CheckBox;
            Func<string> defaultHeaderTitle = () => string.IsNullOrEmpty(gridHeaderTitle) ? " " : gridHeaderTitle;
            GridSetting.GridHeaderTitle = isCheckBoxCell() ? defaultHeaderTitle() : gridHeaderTitle;

            GridSetting.GridColumnWidth = GridColumnWidthSpecifier.Instance
                .RegisterHandler(new CheckboxCellWidthHandler())
                .RegisterHandler(new LinkDetailCellWidthHandler())
                .RegisterHandler(new RegularCellWidthHandler())                
                .RegisterHandler(new StatusHeaderNameWidthHandler())
                .RegisterHandler(new ModalCellWidthHandler())
                .RegisterHandler(new ActionLinkCellWidthHandler())
                .SetWidth(new GridCellWidthContainer
                {
                    GridCellType = gridCellType, Title = gridHeaderTitle,Width = gridColumnWidth
                });

            return this;
        }

        public IGridSettingDataBuilder CellDisplayFormat(string gridCellDisplayFormat = "", string gridCellDisplayText = "")
        {
            GridSetting.GridCellDisplayFormat = gridCellDisplayFormat;
            GridSetting.GridCellDisplayText = gridCellDisplayText;
            return this;
        }

        public IGridSettingDataBuilder CellLinkDetail(int? level = null)
        {
            GridSetting.LinkDetailLevel = level ?? 1;
            return this;
        }

        public IGridSettingDataBuilder CellBehaviour(bool? gridColumnFilterable, bool? gridColumnSortable = false, string gridColumnAlignment = "")
        {
            GridSetting.GridColumnFilterable = gridColumnFilterable ?? false;
            GridSetting.GridColumnSortable = gridColumnSortable ?? false;
            GridSetting.GridColumnAlignment = gridColumnAlignment;
            return this;
        }

        public IGridSettingDataBuilder ControllerProperties(string area = "", string controllerName = "", string controllerAction = "")
        {
            GridSetting.Area = area;
            GridSetting.ControllerName = controllerName;
            GridSetting.ControllerAction = controllerAction;
            return this;
        }

        public IGridSettingDataBuilder ModalProperties(string modalWindowTitle, int? modalWindowWidth = null, int? modalWindowHeight = null)
        {
            GridSetting.ModalWindowTitle = modalWindowTitle;
            GridSetting.ModalWindowWidth = modalWindowWidth ?? 0;
            GridSetting.ModalWindowHeight = modalWindowHeight ?? 0;
            return this;
        }

        public IGridSettingDataBuilder SortOrder(int sortOrder)
        {
            GridSetting.SortOrder = sortOrder;
            return this;
        }

        public IGridSettingDataBuilder GridProperty(string gridName)
        {
            GridSetting.GridName = gridName;
            GridSetting.CreatedBy = "System";
            GridSetting.CreatedDate = DateTime.Now;
            return this;
        }

        public IGridSettingDataBuilder Lockable(bool lockable = true)
        {
            GridSetting.GridColumnLockable = lockable;
            return this;
        }

        public IGridSettingDataBuilder Locked(bool locked = true)
        {
            GridSetting.GridColumnLocked = locked;
            return this;
        }

        public IGridSettingDataBuilder Groupable(bool groupable = true)
        {
            GridSetting.GridColumnGroupable = groupable;
            return this;
        }

        public GridSetting GetGridInstance()
        {
            GridSetting.DataSourceEntityKeyColumnName = OverrideKey ? GridSetting.DataSourceEntityColumnName : GridSetting.DataSourceEntityKeyColumnName;
            GridSetting.GridColumnLockable = GridSetting.GridColumnLockable ?? false;

            _CheckGridName();
            _CheckEntityProperties();

            if (GridSetting.GridCellType == GridCellType.LinkModal.ToString())
            {
                _CheckControllerSettings();

                _CheckModalWindowSettings();
            }
            else if (GridSetting.GridCellType == GridCellType.LinkDetails.ToString())
            {
                _CheckLinkDetail();
            }
            _ForceSorting();
            return GridSetting;
        }

        public bool OverrideKey { get; set; }

        private void _ForceSorting()
        {
            GridSetting.GridColumnSortable = GridSetting.GridCellType != GridCellType.CheckBox.ToString();
        }

        private void _CheckGridName()
        {
            if (string.IsNullOrEmpty(GridSetting.GridName))
            {
                throw new Exception("Missing GridName property! Please call the GridName function.");
            }
        }

        private void _CheckEntityProperties()
        {
            if (string.IsNullOrEmpty(GridSetting.DataSourceEntityType))
            {
                throw new Exception("Please set the DataSourceEntityType property! Please call the EntityType function.");
            }

            if (string.IsNullOrEmpty(GridSetting.DataSourceEntityKeyColumnName))
            {
                throw new Exception("Please set the DataSourceEntityKeyColumnName property! Please call the EntityProperties function.");
            }

            if (string.IsNullOrEmpty(GridSetting.DataSourceEntityColumnName))
            {
                throw new Exception("Please set the DataSourceEntityColumnName property! Please call the EntityProperties function.");
            }
        }

        private void _CheckControllerSettings()
        {
            if (string.IsNullOrEmpty(GridSetting.Area)
                || string.IsNullOrEmpty(GridSetting.ControllerName)
                || string.IsNullOrEmpty(GridSetting.ControllerAction))
            {
                throw new Exception("LinkModal column should have a Area, Controller, and Action properties! Please call the ControllerProperties function.");
            }
        }

        private void _CheckModalWindowSettings()
        {
            if (GridSetting.ModalWindowHeight == null)
            {
                throw new Exception("LinkModal column should have a value for the ModalWindowHeight property! Please call the ModalProperties function.");
            }

            if (GridSetting.ModalWindowWidth == null)
            {
                throw new Exception("LinkModal column should have a value for the ModalWindowWidth property! Please call the ModalProperties function.");
            }

            if (string.IsNullOrEmpty(GridSetting.ModalWindowTitle))
            {
                throw new Exception("LinkModal column should have a value for the ModalWindowTitle property! Please call the ModalProperties function.");
            }
        }

        private void _CheckLinkDetail()
        {
            if (GridSetting.LinkDetailLevel == null || GridSetting.LinkDetailLevel <= 0)
            {
                //throw new Exception("LinkDetails column should have a LinkDetailLevel of 1 or more");
                GridSetting.LinkDetailLevel = 1;
            }
        }
    }
}
