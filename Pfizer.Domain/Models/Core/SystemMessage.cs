using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class SystemMessage : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SystemMessageId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
