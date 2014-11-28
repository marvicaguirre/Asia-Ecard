using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of product")]
    public class Product : AbstractBaseModel
    {
        public Product()
        {
            SalesRetailPrices = new List<SalesRetailPrice>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for product table")]
        public int ProductId { get; set; }
        [MaxLength(250)]
        [ColumnDescription("product name")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("product description")]
        public string Description { get; set; }

        public virtual ICollection<SalesRetailPrice> SalesRetailPrices { get; set; }
    }
}