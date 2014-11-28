using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(ConversionFactorValidator))]
    public class ConversionFactorViewModel : IConversionFactorViewModel
    {
        public int ConversionFactorId { get; set; }
        public string PfizerCode { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int DosageId { get; set; }
        public string DosageForm { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string ProductName { get; set; }
        public decimal Factor { get; set; } 
    }
}