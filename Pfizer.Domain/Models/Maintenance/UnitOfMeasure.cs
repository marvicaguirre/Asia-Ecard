using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of unit of measure")]
    public class UnitOfMeasure : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for unit of measure table")]
        public int UnitOfMeasureId { get; set; }
        [MaxLength(250)]
        [ColumnDescription("unit of measure name")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("unit of measure description")]
        public string Description { get; set; }
    }
}