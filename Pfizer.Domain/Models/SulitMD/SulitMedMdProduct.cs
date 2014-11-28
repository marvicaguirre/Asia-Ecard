using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of SulitMedMdProduct")]
    [Table("SulitMedMDProduct")]
    public class SulitMedMdProduct : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for SulitMedMdProduct table")]
        [Column("SulitMedMDProductId")]
        public int SulitMedMdProductId { get; set; }
        public int SulitMedMdId { get; set; }
        public virtual SulitMedMd SulitMedMd { get; set; }
        public int SulitMedProductId { get; set; }
        public virtual SulitMedProduct SulitMedProduct { get; set; }
        [Column("Qty")]
        public int Quantity { get; set; }
        [MaxLength(1)]
        [Column(TypeName = "char")]
        public string Frequency { get; set; }
    }
}
