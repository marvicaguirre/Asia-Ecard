using System;

namespace Wizardsgroup.Core.Web.Models
{
    public class GridSettingViewModel
    {        
        public int GridSettingId { get; set; }

        public string DataSourceEntityType { get; set; }

        public string DataSourceEntityKeyColumnName { get; set; }

        public string DataSourceEntityColumnName { get; set; }

        public string ControllerAction { get; set; }

        public string ControllerName { get; set; }

        public string ModalWindowTitle { get; set; }

        public int? ModalWindowHeight { get; set; }

        public int? ModalWindowWidth { get; set; }

        public string GridHeaderTitle { get; set; }

        public string GridName { get; set; }

        public int? GridColumnWidth { get; set; }

        public bool? GridColumnFilterable { get; set; }

        public bool? GridColumnSortable { get; set; }

        public bool? GridColumnMenu { get; set; }
        
        public bool? GridColumnLocked { get; set; }

        public bool? GridColumnLockable { get; set; }

        public bool? GridColumnGroupable{ get; set; }
        
        public string GridCellDisplayFormat { get; set; }

        public string GridColumnAlignment { get; set; }

        public string GridCellDisplayText { get; set; }

        public int? LinkDetailLevel { get; set; }

        public string GridCellType { get; set; }

        public string Area { get; set; }

        public int? CellGroup { get; set; }
    }
}