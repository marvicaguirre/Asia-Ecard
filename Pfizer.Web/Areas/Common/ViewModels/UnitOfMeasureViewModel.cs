﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(UnitOfMeasureValidator))]
    public class UnitOfMeasureViewModel : IUnitOfMeasureViewModel
    {
        public int UnitOfMeasureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}