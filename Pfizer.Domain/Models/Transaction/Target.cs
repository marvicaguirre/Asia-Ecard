using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of Target")]
    public class Target : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for Target table")]
        public int TargetId { get; set; }
        public int FiscalYear { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "varchar")]
        public string TeamCode { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public int TargetTypeId { get; set; }
        public virtual TargetType TargetType { get; set; }

        #region Months

        public int Month1 { get; set; }
        public int Month2 { get; set; }
        public int Month3 { get; set; }
        public int Month4 { get; set; }
        public int Month5 { get; set; }
        public int Month6 { get; set; }
        public int Month7 { get; set; }
        public int Month8 { get; set; }
        public int Month9 { get; set; }
        public int Month10 { get; set; }
        public int Month11 { get; set; }
        public int Month12 { get; set; }

        #endregion
    }
}
