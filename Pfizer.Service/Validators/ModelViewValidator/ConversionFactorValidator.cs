using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class ConversionFactorValidator : AbstractViewModelValidator<IConversionFactorViewModel>
    {
        public ConversionFactorValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.PfizerCode)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicatePfizerCode)
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.UnitOfMeasureId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicateUnitOfMeasure)
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Factor)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThan(0);
        }
        private bool HasNoDuplicatePfizerCode(IConversionFactorViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<ConversionFactor>();
            var hasResult = !repository.Query()
                .Filter(o=>o.ConversionFactorId != x.ConversionFactorId)
                .Filter(o => o.PfizerCode == e.Trim())
                .GetResult().Any();
            return hasResult;
        }

        private bool HasNoDuplicateUnitOfMeasure(IConversionFactorViewModel x, int e)
        {
            var repository = UnitOfWork.Repository<ConversionFactor>();
            var hasResult = !repository.Query()
                .Filter(o=>o.ConversionFactorId != x.ConversionFactorId)
                .Filter(o=>o.DosageId == x.DosageId)
                .Filter(o => o.UnitOfMeasureId == x.UnitOfMeasureId)
                .GetResult().Any();
            return hasResult;
        }
    }
}
