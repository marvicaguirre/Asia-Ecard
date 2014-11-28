using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of TSST_RAW")]
    [Table("TSST_RAW")]
    public class TsstRaw : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for TSST_RAW table")]
        public int DumpDataId { get; set; }
        [MaxLength(12)]
        [Column("TSST", TypeName = "varchar")]
        public string Tsst { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string CardNo { get; set; }
        [MaxLength(10)]
        [Column("PRC", TypeName = "char")]
        public string Prc { get; set; }
        public DateTime? CDate { get; set; }   
    }
}
