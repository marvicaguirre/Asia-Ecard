using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of sales configuration per fiscal year per territory")]
    public class TerritorySalesConfiguration : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for sales configuration table")]
        public int TerritorySalesConfigurationId { get; set; }
        [ColumnDescription("fiscal year")]
        public int FiscalYear { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int RegionId { get; set; }
        public string DistrictId { get; set; }
        [MaxLength(3)]
        [ColumnDescription("the code of the territory")]
        public string TerritoryCode { get; set; }
        [MaxLength(100)]
        [ColumnDescription("the name of the territory")]
        public string TerritoryName { get; set; }
    }
}