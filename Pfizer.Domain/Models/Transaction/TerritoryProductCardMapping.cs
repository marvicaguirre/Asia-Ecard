using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    public class TerritoryProductCardMapping : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TerritoryProductCardMappingId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OldTeamId { get; set; }
        public virtual Team OldTeam { get; set; }
        public int NewTeamId { get; set; }
        public virtual Team NewTeam { get; set; }
    }
}
