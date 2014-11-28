using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of customers for a specific store branch")]
    public class CustomerMapping : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for class table")]
        public int CustomerId { get; set; }
        [ColumnDescription("Id of the Branch where the customer is associated with")]
        public int StoreBranchId { get; set; }
        public virtual StoreBranch Branch { get; set; }
    }
}