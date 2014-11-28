using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of type of documents")]
    public class DocumentType : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for document type table")]
        public int DocumentTypeId { get; set; }
        [MaxLength(250)]
        [ColumnDescription("name of type of document")]
        public string Name { get; set; }
    }
}