using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of card type")]
    public class CardType : AbstractBaseModel
    {
        public CardType()
        {
            Programs = new List<Program>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for card type table")]
        public int CardTypeId { get; set; }
        [ColumnDescription("Id of the class linked to card type")]
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        [MaxLength(250)]
        [ColumnDescription("card type name")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("card type description")]
        public string Description { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}