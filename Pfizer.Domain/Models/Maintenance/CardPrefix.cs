using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of prefixes for the card type")]
    public class CardPrefix : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for prefix table")]
        public int CardPrefixId { get; set; }
        [ColumnDescription("Id of the program product linked to a prefix")]
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        [MaxLength(250)]
        [ColumnDescription("prefix of the card type")]
        public string Name { get; set; }
    }
}