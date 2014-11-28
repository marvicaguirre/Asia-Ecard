using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of TargetType")]
    public class TargetType : AbstractBaseModel
    {
        public TargetType()
        {
            Targets = new List<Target>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for TargetType table")]
        public int TargetTypeId { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        [ColumnDescription("TargetType name")]
        public string Name { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}
