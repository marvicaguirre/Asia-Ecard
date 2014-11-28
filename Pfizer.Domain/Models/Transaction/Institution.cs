using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of institution")]
    public class Institution : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for institution table")]
        public int InstitutionId { get; set; }
        [ColumnDescription("Id of the institution type linked to institution")]
        public int InstitutionTypeId { get; set; }
        public virtual InstitutionType InstitutionType { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        [ColumnDescription("institution name")]
        public string Name { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [ColumnDescription("institution shortname")]
        public string ShortName { get; set; }
        public int HmoId { get; set; }
        
        #region Address
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string Address1 { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string Address2 { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string Address3 { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string Address4 { get; set; } 
        #endregion

        #region Doctor, Nurse and Contact Person
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string Doctor { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string DoctorSchedule { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string Nurse { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string NurseSchedule { get; set; }
        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string ContactPerson { get; set; }
        [MaxLength(300)]
        [Column(TypeName = "varchar")]
        public string ContactNumber { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }
        #endregion
        
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string DomainName { get; set; }
        public int? EmployeeCount { get; set; }
        [MaxLength(10)]
        [Column(TypeName = "char")]
        public string Territory { get; set; }
        
    }
}
