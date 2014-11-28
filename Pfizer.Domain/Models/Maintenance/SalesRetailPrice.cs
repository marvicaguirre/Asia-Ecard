using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list price per product per dosage form")]
    public class SalesRetailPrice : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for sales retail price table")]
        public int SalesRetailPriceId { get; set; }        
        [ColumnDescription("default value is 'Sales Retail Price'")]
        public int PriceTypeId { get; set; }
        public virtual PriceType PriceType { get; set; }
        [ColumnDescription("dosage id that is linked to sales retail price")]
        public int DosageId { get; set; }
        public virtual Dosage Dosage { get; set; }
        [ColumnDescription("product id that is linked to sales retail price")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ColumnDescription("This is where the user will enter the starting effectivity date of the sales retail price. Default value is current date")]
        public DateTime? From { get; set; }
        [ColumnDescription("This is where the user will enter the end date of the sales retail price. If there is no ending date it means the price is the current SRP")]
        public DateTime? To { get; set; }
        [ColumnDescription("This is where the user will enter the sales retail price")]
        public decimal Price { get; set; }

    }
}