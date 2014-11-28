using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of price type. this is a system table")]
    public class PriceType : AbstractBaseModel
    {
        public PriceType()
        {
            SalesRetailPrices = new List<SalesRetailPrice>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for class table")]
        public int PriceTypeId { get; set; }
        [MaxLength(250)]
        [ColumnDescription("price type name")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("price type description")]
        public string Description { get; set; }

        public virtual ICollection<SalesRetailPrice> SalesRetailPrices { get; set; }
    }
}