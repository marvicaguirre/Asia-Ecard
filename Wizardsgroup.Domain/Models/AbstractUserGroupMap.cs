using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public abstract class AbstractUserGroupMap : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupMapId { get; set; }
        public int? UserId { get; set; }
        public virtual AbstractUser AbstractUser { get; set; }
        public int? UserGroupId { get; set; }
        public virtual AbstractUserGroup AbstractUserGroup { get; set; }
    }
}
