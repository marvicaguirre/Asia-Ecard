using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(CardTypePrefixValidator))]
    public class CardPrefixViewModel : ICardPrefixViewModel
    {
        public int CardPrefixId { get; set; }
        public int ProgramId { get; set; }
        public string  ProgramName { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}