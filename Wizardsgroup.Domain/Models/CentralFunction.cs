using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public partial class CentralFunction : AbstractBaseModel
    {
        public CentralFunction()
        {
            UserGroupFunctions = new HashSet<UserGroupFunction>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CentralFunctionId { get; set; }
        [MaxLength(100)]
        public string FunctionName { get; set; }
        [MaxLength(200)]
        public string DisplayName { get; set; }
        [MaxLength(100)]
        public string ShortDisplayName { get; set; }
        public virtual ICollection<UserGroupFunction> UserGroupFunctions { get; set; }
        public int? CentralModuleId { get; set; }        
        public virtual CentralModule CentralModule { get; set; }
    }
}
