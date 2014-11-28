using System;
using System.Data;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class ProgramValidator : AbstractViewModelValidator<IProgramViewModel>
    {
        public ProgramValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicate)
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();

            RuleFor(o => o.CardTypeId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();

            RuleFor(o => o.VendorCode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicateVendorCode)
                .WithMessage("{PropertyName} must be unique.");
        }

        private bool HasNoDuplicateVendorCode(IProgramViewModel arg1, string arg2)
        {
            var repository = UnitOfWork.Repository<Program>();
            var hasResult = !repository.Query()
                .Filter(o => o.ProgramId != arg1.ProgramId)
                .Filter(o => o.VendorCode == arg2.Trim())
                .GetResult().Any();

            return hasResult;
        }

        private bool HasNoDuplicate(IProgramViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<Program>();
            var hasResult = !repository.Query()
                .Filter(o => o.ProgramId != x.ProgramId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();

            return hasResult;
        }
    }
}
