using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(DosageValidator))]
    public class DosageViewModel : IDosageViewModel
    {
        public int DosageId { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int NoOfCoversionFactor { get; set; }
        public string  Status { get; set; }
    }
}