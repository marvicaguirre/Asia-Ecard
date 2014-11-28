using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public abstract class AbstractUser : AbstractBaseModel
    {
        public AbstractUser()
        {            
            UserGroupMaps = new List<AbstractUserGroupMap>();
            UserGroups = new List<AbstractUserGroup>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [MaxLength(200)]
        public string UserName { get; set; }
        [MaxLength(500)]
        public string Password { get; set; }
        public virtual List<AbstractUserGroupMap> UserGroupMaps { get; set; }
        public virtual List<AbstractUserGroup> UserGroups { get; set; }
    }
}
