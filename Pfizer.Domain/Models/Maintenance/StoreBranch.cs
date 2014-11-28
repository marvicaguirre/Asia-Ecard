using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Domain.Base;

namespace Pfizer.Domain.Models
{
    [TableDescription("list of StoreBranch")]
    public class StoreBranch : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDescription("Primary key for StoreBranch table")]
        public int StoreBranchId { get; set; }

        #region Location
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Address1 { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Address2 { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Address3 { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Address4 { get; set; }
        #endregion

        #region Store
        public int StoreAreaId { get; set; }
        public virtual StoreArea StoreArea { get; set; }
        public int StoreMainId { get; set; }
        public virtual StoreMain StoreMain { get; set; }
        #endregion

        [MaxLength(10)]
        [Column(TypeName = "varchar")]
        public string Code { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "varchar")]
        public string ContactPerson { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string BusinessNumber { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string MobileNumber { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string FaxNumber { get; set; }
        [MaxLength(200)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string StoreHours { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string ConnectionType { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string AssignedVendor { get; set; }

        public DateTime? DateInstalled { get; set; }
    }
}
