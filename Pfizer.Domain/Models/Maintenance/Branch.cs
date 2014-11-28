using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of branch")]
    public class Branch : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for class table")]        
        public int BranchId { get; set; }
        [ColumnDescription("Id of the Company where the branch is associated with")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [MaxLength(250)]
        [ColumnDescription("branch name")]
        public string Name { get; set; }
        [MaxLength(1000)]
        [ColumnDescription("branch description")]
        public string Description { get; set; }
        [MaxLength(250)]
        [ColumnDescription("Zuelig code")]
        public string ZueligCode { get; set; }
    }
}