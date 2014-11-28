using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of SulitMedMd")]
    [Table("SulitMedMD")]
    public class SulitMedMd : AbstractBaseModel
    {
        public SulitMedMd()
        {
            SulitMedMdProducts = new List<SulitMedMdProduct>();            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for SulitMedMd table")]
        [Column("SulitMedMDId")]
        public int SulitMedMdId { get; set; }
        [MaxLength(10)]
        [Column("PRC",TypeName = "char")]
        public string Prc { get; set; }
        [MaxLength(200)]
        [Column(TypeName = "varchar")]
        public string AlternateConsignee { get; set; }
        [Column("IsTSST")]
        public bool? IsTsst { get; set; }

        public virtual ICollection<SulitMedMdProduct> SulitMedMdProducts { get; set; }
    }
}
