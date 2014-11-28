using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public abstract class AbstractUserGroup : AbstractBaseModel
    {
        public AbstractUserGroup()
        {
            UserGroupMaps = new List<AbstractUserGroupMap>();
            UserGroupFunctions = new List<UserGroupFunction>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public List<AbstractUserGroupMap> UserGroupMaps { get; set; }
        public List<UserGroupFunction> UserGroupFunctions { get; set; }
    }
}
