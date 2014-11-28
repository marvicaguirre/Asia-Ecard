using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of province")]
    public class Province : AbstractBaseModel
    {
        public Province()
        {
            Cities = new List<City>(); 
            StoreBranches = new List<StoreBranch>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for province table")]
        public int ProvinceId { get; set; }
        [MaxLength(255)]
        [ColumnDescription("province name")]
        [Column("Province")]
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<StoreBranch> StoreBranches { get; set; }

    }
}
