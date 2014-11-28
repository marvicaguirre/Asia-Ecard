using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of Pfizer codes and conversion factor")]
    public class ConversionFactor : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for Pfizer codes and conversion factor table")]
        public int ConversionFactorId { get; set; }
        [ColumnDescription("dosage id that is linked to conversion factor")]
        public int DosageId { get; set; }
        public virtual Dosage Dosage { get; set; }        
        //[ColumnDescription("product id that is linked to conversion factor")]
        //public int ProductId { get; set; }
        //public virtual Product Product { get; set; }
        [MaxLength(250)]
        [ColumnDescription("pfizer code")]
        public string PfizerCode { get; set; }
        [ColumnDescription("unit of measure id that is linked to conversion factor")]
        public int UnitOfMeasureId { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        [ColumnDescription("quantity per unit of measure")]
        public decimal Factor { get; set; }
    }
}