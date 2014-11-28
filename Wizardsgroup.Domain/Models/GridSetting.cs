using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public class GridSetting : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GridSettingId { get; set; }
        [MaxLength(200)]
        public string DataSourceEntityType { get; set; }
        [MaxLength(100)]
        public string DataSourceEntityKeyColumnName { get; set; }
        [MaxLength(100)]
        public string GridName { get; set; }
        [MaxLength(100)]
        public string DataSourceEntityColumnName { get; set; }
        [MaxLength(50)]
        public string GridCellType { get; set; }
        [MaxLength(100)]
        public string EntityPropertyName { get; set; }
        public Nullable<int> LinkDetailLevel { get; set; }
        [MaxLength(100)]
        public string GridHeaderTitle { get; set; }
        [MaxLength(100)]
        public string GridColumnName { get; set; }
        public Nullable<int> GridColumnWidth { get; set; }
        public Nullable<bool> GridColumnFilterable { get; set; }
        [MaxLength(100)]
        public string GridColumnAlignment { get; set; }
        public Nullable<bool> GridColumnSortable { get; set; }
        public Nullable<bool> GridColumnLocked { get; set; }
        public Nullable<bool> GridColumnLockable { get; set; }
        public Nullable<bool> GridColumnGroupable { get; set; }
        public Nullable<bool> GridColumnMenu { get; set; }
        [MaxLength(100)]
        public string GridCellDisplayFormat { get; set; }
        [MaxLength(100)]
        public string GridCellDisplayText { get; set; }
        public Nullable<int> SortOrder { get; set; }
        [MaxLength(100)]
        public string Area { get; set; }
        public Nullable<int> CellGroup { get; set; }
        [MaxLength(100)]
        public string ControllerName { get; set; }
        [MaxLength(100)]
        public string ControllerAction { get; set; }
        [MaxLength(100)]
        public string ModalWindowTitle { get; set; }
        public Nullable<int> ModalWindowWidth { get; set; }
        public Nullable<int> ModalWindowHeight { get; set; }
    }
}
