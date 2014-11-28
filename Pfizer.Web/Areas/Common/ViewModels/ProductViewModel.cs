using FluentValidation.Attributes;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Service.Validators.ModelViewValidator;

namespace Pfizer.Web.Areas.Common.ViewModels
{
    [Validator(typeof(ProductValidator))]
    public class ProductViewModel : IProductViewModel
    {
        public int ProductId { get; set; }
        public int UniqueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}