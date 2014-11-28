using System;
using System.Linq;
using FluentValidation;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class UserValidator : AbstractViewModelValidator<IUser>
    {
        public UserValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.EmployeeId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must((x,e) => EmployeeHasNoAccess(x,x.EmployeeId))
                .WithMessage("{PropertyName} already have a User Name and Password");

            RuleFor(o => o.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicateUserName)
                .WithMessage("{PropertyName} must be unique");

            RuleFor(o => o.UserPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }

        private bool HasNoDuplicateUserName(IUser x, string e)
        {
            var repository = UnitOfWork.Repository<User>();
            var hasResult = !repository.Query()
                    .Filter(o => o.UserName == e.Trim() && o.EmployeeId != x.EmployeeId)
                    .GetResult().Any();

            return hasResult;
        }

        private bool EmployeeHasNoAccess(IUser x, int? e)
        {
            bool hasResult;
            var repository = UnitOfWork.Repository<User>();

            if (x.UserId == default(int))
            {
                hasResult = !repository.Query()
                                .Filter(o => o.EmployeeId == e)
                                .GetResult().Any();
            }
            else
            {
                hasResult = !repository.Query()
                                .Filter(o => o.EmployeeId == e && o.UserId != x.UserId)
                                .GetResult().Any();
            }

            return hasResult;
        }

    }
}
