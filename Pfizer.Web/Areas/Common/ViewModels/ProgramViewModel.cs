using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(ProgramValidator))]
    public class ProgramViewModel : IProgramViewModel
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VendorCode { get; set; }
        public int CardTypeId { get; set; }
        public string CardTypeName { get; set; }
        public int CardIdCount { get; set; }
        public string Status{ get; set; }
    }
}