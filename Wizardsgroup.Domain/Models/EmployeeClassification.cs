﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wizardsgroup.Domain.Base;

namespace Wizardsgroup.Domain.Models
{
    public class EmployeeClassification : AbstractBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeClassificationId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}