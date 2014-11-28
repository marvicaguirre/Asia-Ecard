using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of program")]
    public class Program : AbstractBaseModel
    {
        public Program()
        {
            Targets = new List<Target>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for program table")]
        public int ProgramId { get; set; }
        [ColumnDescription("type of card associated with the program")]
        public int CardTypeId { get; set; }
        public virtual CardType CardType { get; set; }
        [MaxLength(250)]
        [ColumnDescription("name of the program")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("description of the program")]
        public string Description { get; set; }
        [MaxLength(250)]
        [ColumnDescription("vendor code of the program")]
        public string VendorCode { get; set; }

        public virtual ICollection<Target> Targets { get; set; }
    }
}