using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of type of uploaded documents")]
    public class Document : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for document type table")]
        public int DocumentId { get; set; }
        [ColumnDescription("Id of the document type linked to the document")]
        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        [MaxLength(250)]
        [ColumnDescription("document filename")]
        public string FileName { get; set; }
        [MaxLength(250)]
        [ColumnDescription("file extension")]
        public string FileExtension { get; set; }
        [ColumnDescription("file extension")]
        public byte[] File { get; set; }
        [MaxLength(50)]
        [ColumnDescription("file processing result")]
        public string ProcessResult { get; set; }
    }
}