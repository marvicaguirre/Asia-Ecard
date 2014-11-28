using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class DosageValidator : AbstractViewModelValidator<IDosageViewModel>
    {
        public DosageValidator(Func<IUnitOfWork> unitOfWorkFunction) : base(unitOfWorkFunction)
        {
            RuleFor(o => o.UniqueId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicateDosageId)
               .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicateForm)
                //TODO: Dosage Form must be unique per Product Name
               .WithMessage("{PropertyName} must be unique.");
        }

        private bool HasNoDuplicateDosageId(IDosageViewModel x, string e)
        {
            var repository = UnitOfWork.Repository<Dosage>();
            var hasResult = !repository.Query()
                .Filter(o => o.DosageId != x.DosageId)
                .Filter(o => o.UniqueId == e)
                .GetResult().Any();
            return hasResult;
        }
        private bool HasNoDuplicateForm(IDosageViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<Dosage>();
            var hasResult = !repository.Query()
                .Filter(o => o.DosageId != x.DosageId)
                .Filter(o => o.ProductId == x.ProductId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();
            return hasResult;
        }
    }
}
