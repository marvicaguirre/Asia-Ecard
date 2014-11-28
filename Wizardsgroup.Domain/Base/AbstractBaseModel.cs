using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Enumerations;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Domain.Base
{
    public abstract class AbstractBaseModel : IActiveRecord
    {
        public AbstractBaseModel()
        {            
            CreatedDate = DateTime.Now;
        }

        [ColumnDescription("The username of the record creator.")]
        [MaxLength(250)]
        public string CreatedBy { get; set; }
        [ColumnDescription("The date and time the record was created.")]
        public DateTime CreatedDate { get; set; }
        [ColumnDescription("The username of the record modifier.")]
        [MaxLength(250)]
        public string ModifiedBy { get; set; }
        [ColumnDescription("The  date and time the record was modified.")]
        public DateTime? ModifiedDate { get; set; }
        [ColumnDescription("The record's active/inactive status.", SampleData ="0 = Active, 1 = Inactive")]
        public RecordStatus RecordStatus { get; set; }
        [NotMapped]
        public string Status { get { return RecordStatus == RecordStatus.Active ? "Active" : "Inactive"; } }
    }
}
