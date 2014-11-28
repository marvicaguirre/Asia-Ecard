using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(SalesRetailPriceValidator))]
    public class SalesRetailPriceViewModel : ISalesRetailPriceViewModel
    {
        public int SalesRetailPriceId { get; set; }
        public int PriceTypeId { get; set; }
        public string PriceTypeName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DosageId { get; set; }
        public string DosageForm { get; set; }

        //TODO:Kevin Where to get this?
        public string ConversionPerUnit { get; set; }
    }
}