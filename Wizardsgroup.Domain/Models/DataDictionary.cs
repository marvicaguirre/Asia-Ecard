using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public class DataDictionary : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DataDictionaryId { get; set; }
        [MaxLength(100)]
        public string Model { get; set; }
        [MaxLength(100)]
        public string FieldName { get; set; }
        [MaxLength(100)]
        public string FieldDisplayText { get; set; }        
        public bool IsRequired { get; set; }
    }
}
