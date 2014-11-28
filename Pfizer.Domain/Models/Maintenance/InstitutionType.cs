using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of institutiontype")]
    public class InstitutionType : AbstractBaseModel
    {
        public InstitutionType()
        {
            Institutions = new List<Institution>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for institutiontype table")]
        public int InstitutionTypeId { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        [ColumnDescription("institutiontype name")]
        public string Name { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
