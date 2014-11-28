using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of city")]
    public class City : AbstractBaseModel
    {
        public City()
        {
            StoreBranches = new List<StoreBranch>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for class table")]
        public int CityId { get; set; }
        [MaxLength(255)]
        [ColumnDescription("city name")]
        [Column("City")]
        public string Name { get; set; }
        [ColumnDescription("Id of the province where the city is associated with")]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<StoreBranch> StoreBranches { get; set; }
    }
}
