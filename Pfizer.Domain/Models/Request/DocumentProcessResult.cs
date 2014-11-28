using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of error messages from the processing of uploaded excel")]
    public class DocumentProcessResult : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for document process result table")]
        public int DocumentProcessResultId { get; set; }

        [ColumnDescription("Id of the document linked to document process result")]
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
        [ColumnDescription("show the row number that has an error in the uploaded excel file")]
        public int Row { get; set; }
        [MaxLength(2000)]
        [ColumnDescription("the information on what the error is on the specified row number")]
        public string Remarks { get; set; }

    }
}