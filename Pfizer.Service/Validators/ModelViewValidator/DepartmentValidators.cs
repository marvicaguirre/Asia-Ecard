using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class DepartmentValidators : AbstractViewModelValidator<IDepartment>
    {
        public DepartmentValidators(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
           
            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must((x, e) => HasNoDuplicateDepartment(x, e))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Description)
                .NotEmpty();

        }

        private bool HasNoDuplicateDepartment(IDepartment x, string e)
        {
            var repository = UnitOfWork.Repository<Department>();
            var hasNoResult = !repository.Query()
                .Filter(o => o.DepartmentId != x.DepartmentId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();
            
            return hasNoResult;
        }
    }
}
