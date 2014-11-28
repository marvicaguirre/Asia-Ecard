using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of products for a specific program")]
    public class ProgramProductMapping : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for program product table")]
        public int ProgramProductMappingId { get; set; }
        [ColumnDescription("Id of the program product linked to a program")]
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        [ColumnDescription("list of products associated to a program")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}