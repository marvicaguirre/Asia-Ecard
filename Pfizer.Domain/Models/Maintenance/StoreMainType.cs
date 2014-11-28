using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of StoreMainType")]
    public class StoreMainType : AbstractBaseModel
    {
        public StoreMainType()
        {
            StoreMains = new List<StoreMain>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for StoreMainType table")]
        public int StoreMainTypeId { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        [ColumnDescription("StoreMainType name")]
        public string Name { get; set; }

        public virtual ICollection<StoreMain> StoreMains { get; set; }
    }
}
