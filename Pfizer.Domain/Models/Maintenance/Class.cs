using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of card class")]
    public class Class : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for class table")]
        public int ClassId { get; set; }
        [MaxLength(250)]
        [ColumnDescription("class name")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("class description")]
        public string Description { get; set; }
    }
}
