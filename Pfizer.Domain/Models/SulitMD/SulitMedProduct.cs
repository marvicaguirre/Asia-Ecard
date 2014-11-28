using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of SulitMedProduct")]
    public class SulitMedProduct : AbstractBaseModel
    {
        public SulitMedProduct()
        {
            SulitMedMdProducts = new List<SulitMedMdProduct>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for SulitMedProduct table")]
        public int SulitMedProductId { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<SulitMedMdProduct> SulitMedMdProducts { get; set; }
    }
}
