using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public class AuditLog : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditLogId { get; set; }
        public Guid GroupId { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        public DateTime EventDateUtc { get; set; }
        [MaxLength(100)]
        public string EventType { get; set; }
        [MaxLength(100)]
        public string TableName { get; set; }
        [MaxLength(18)]
        public string RecordId { get; set; }
        [MaxLength(100)]
        public string ColumnName { get; set; }        
        public string OriginalValue { get; set; }        
        public string NewValue { get; set; }
    }
}
