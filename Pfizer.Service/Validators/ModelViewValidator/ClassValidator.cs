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
    public class ClassValidator : AbstractViewModelValidator<IClassViewModel>
    {
        public ClassValidator(Func<IUnitOfWork> unitOfWorkFunction) : base(unitOfWorkFunction)
        {
            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicate)
               .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }

        private bool HasNoDuplicate(IClassViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<Class>();
            var hasResult = !repository.Query()
                .Filter(o => o.ClassId != x.ClassId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();

            return hasResult;
        }
    }
}
