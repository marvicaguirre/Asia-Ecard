using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of StoreMainHandlingFee")]
    public class StoreMainHandlingFee : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for StoreMainHandlingFee table")]
        public int StoreMainHandlingFeeId { get; set; }
        public int StoreMainId { get; set; }
        public virtual StoreMain StoreMain { get; set; }
        [Column(TypeName = "float")]
        public double HandlingFeeRate { get; set; }

        public int FromFiscalYear { get; set; }
        public int FromFiscalMonth { get; set; }
        public int ToFiscalYear { get; set; }
        public int ToFiscalMonth { get; set; }
    }
}
