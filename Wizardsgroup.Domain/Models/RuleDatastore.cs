using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public class RuleDatastore : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RuleDatastoreId { get; set; }
        [MaxLength(50)]
        public string Controller { get; set; }
        [MaxLength(50)]
        public string ControllerAction { get; set; }
        [MaxLength(50)]
        public string Field { get; set; }
        [MaxLength(50)]
        public string RuleOperator { get; set; }
        [MaxLength(50)]
        public string Value { get; set; }
        [MaxLength(500)]
        public string ValidationMessage { get; set; }
    }
}
