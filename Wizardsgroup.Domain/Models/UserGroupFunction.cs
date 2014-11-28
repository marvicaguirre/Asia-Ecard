using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public partial class UserGroupFunction : AbstractBaseModel
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupFunctionId { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public int? CentralFunctionId { get; set; }
        public virtual CentralFunction CentralFunction { get; set; }
        public int? UserGroupId { get; set; }
        public virtual AbstractUserGroup AbstractUserGroup { get; set; }
    }
}
