using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of StoreArea")]
    public class StoreArea : AbstractBaseModel
    {
        public StoreArea()
        {
            StoreBranches = new List<StoreBranch>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for StoreArea table")]
        public int StoreAreaId { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        [ColumnDescription("StoreArea name")]
        public string Name { get; set; }

        public virtual ICollection<StoreBranch> StoreBranches { get; set; }        
    }
}
