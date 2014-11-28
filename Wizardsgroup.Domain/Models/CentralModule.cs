using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public partial class CentralModule : AbstractBaseModel
    {
        public CentralModule()
        {
            CentralFunctions = new HashSet<CentralFunction>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CentralModuleId { get; set; }
        [MaxLength(100)]
        public string ModuleName { get; set; }
        [MaxLength(150)]
        public string DisplayName { get; set; }
        [MaxLength(100)]
        public string ShortDisplayName { get; set; }

        public virtual ICollection<CentralFunction> CentralFunctions { get; set; }
    }
}
