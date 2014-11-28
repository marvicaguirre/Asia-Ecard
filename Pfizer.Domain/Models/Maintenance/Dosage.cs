using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of dosage")]
    public class Dosage : AbstractBaseModel
    {
        public Dosage()
        {
            SalesRetailPrices = new List<SalesRetailPrice>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for dosage table")]
        public int DosageId { get; set; }

        [ColumnDescription("Id of the product linked to the dosage")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ColumnDescription("dosage id")]
        public string UniqueId { get; set; }

        [MaxLength(1000)]
        [ColumnDescription("dosage form")]
        public string Name { get; set; }

        public virtual ICollection<SalesRetailPrice> SalesRetailPrices { get; set; }
    }
}