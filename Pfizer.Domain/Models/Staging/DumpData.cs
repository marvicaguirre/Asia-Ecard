using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of dumpdata")]
    public class DumpData : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for dumpdata table")]
        public int DumpDataId { get; set; }
        [MaxLength(12)]
        [Column(TypeName = "varchar")]
        public string TSST { get; set; }
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string CardNo { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "char")]
        public string PRC { get; set; }
        public DateTime? CDate { get; set; }    
        
    }
}
