using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public  class SystemSetting : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SystemSettingId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string DataType { get; set; }
    }
}
