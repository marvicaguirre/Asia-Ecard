using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Models;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class EmployeeValidator : AbstractViewModelValidator<IEmployee>
    {
        public EmployeeValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must((x, e) => HasNoDuplicateEmployee(x))
                .WithMessage("{PropertyName} name should be unique.");

            RuleFor(o => o.LastName)
                .NotEmpty();

            RuleFor(o => o.FirstName)
                .NotEmpty();

            RuleFor(o => o.MiddleName)
                .NotEmpty();

            RuleFor(o => o.CompanyId)
                .NotEmpty();

            RuleFor(o => o.DepartmentId)
                .NotEmpty();

            RuleFor(o => o.EmployeeTypeId)
               .NotEmpty();
        }

        private bool HasNoDuplicateEmployee(IEmployee args)
        {
            var repository = UnitOfWork.Repository<Employee>();

            var hasNoResult = !repository.Query()
                .Filter(o => o.EmployeeId != args.EmployeeId)
                .Filter(o => o.FirstName == args.FirstName.Trim())
                .Filter(o => o.LastName == args.LastName.Trim())
                .Filter(o => o.MiddleName == args.MiddleName.Trim())
                .GetResult().Any();

            return hasNoResult;
        }
    }
}