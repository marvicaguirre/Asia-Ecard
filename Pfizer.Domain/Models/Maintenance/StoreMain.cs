using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of StoreMain")]
    public class StoreMain : AbstractBaseModel
    {
        public StoreMain()
        {
            StoreMainHandlingFees = new List<StoreMainHandlingFee>();
            StoreBranches = new List<StoreBranch>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for StoreMain table")]
        public int StoreMainId { get; set; }
        public int StoreMainTypeId { get; set; }
        public virtual StoreMainType StoreMainType { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }
        [MaxLength(4)]
        [Column(TypeName = "varchar")]
        public string PrefixCode { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string VendorCode { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "varchar")]
        public string SRCode { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string SRName { get; set; }

        public virtual ICollection<StoreMainHandlingFee> StoreMainHandlingFees { get; set; }
        public virtual ICollection<StoreBranch> StoreBranches { get; set; }
    }
}
