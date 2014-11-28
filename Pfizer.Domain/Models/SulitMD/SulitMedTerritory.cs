using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of SulitMedTerritory")]
    public class SulitMedTerritory : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for SulitMedTerritory table")]
        public int SulitMedTerritoryId { get; set; }

        public int FiscalYear { get; set; }
        [MaxLength(5)]
        [Column(TypeName = "varchar")]
        public string TeamCode { get; set; }
        [MaxLength(5)]
        [Column(TypeName = "varchar")]
        public string TerritoryCode { get; set; }
        [Column("IsSLMC")]
        public bool IsSlmc { get; set; }
    }
}
