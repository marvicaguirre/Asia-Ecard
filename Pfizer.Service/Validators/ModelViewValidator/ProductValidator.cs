using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class ProductValidator : AbstractViewModelValidator<IProductViewModel>
    {
        public ProductValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicateName)
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }


        private bool HasNoDuplicateCode(IProductViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<Product>();
            var hasResult = !repository.Query()
                .Filter(o => o.ProductId != x.ProductId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();
            return hasResult;
        }

        private bool HasNoDuplicateName(IProductViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<Product>();
            var hasResult = !repository.Query()
                .Filter(o => o.ProductId != x.ProductId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();
            return hasResult;
        }
    }
}
