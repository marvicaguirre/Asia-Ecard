using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public abstract class AbstractCompany : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int CompanyClassificationId { get; set; }
        public virtual CompanyClassification CompanyClassification { get; set; }

        public DateTime? EffectivityDate { get; set; }
        public DateTime? ValidityDate { get; set; }
    }
}
