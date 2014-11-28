using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class Team : AbstractBaseModel
    {
        public Team()
        {
            OldTerritoryProductCardMappings = new List<TerritoryProductCardMapping>();
            NewTerritoryProductCardMappings = new List<TerritoryProductCardMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }
        public virtual ICollection<TerritoryProductCardMapping> OldTerritoryProductCardMappings { get; set; }
        public virtual ICollection<TerritoryProductCardMapping> NewTerritoryProductCardMappings { get; set; }
    }
}
