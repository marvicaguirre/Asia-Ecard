using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class UserGroupValidator : AbstractViewModelValidator<IUserGroup>
    {
        public UserGroupValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()                
                .Must(HasNoDuplicateName)
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(o => o.Description)
                .NotEmpty();
        }



        private bool HasNoDuplicateName(IUserGroup x, string e)
        {
            var repository = UnitOfWork.Repository<UserGroup>();
            var hasResult = !repository.Query()
                .Filter(o => o.UserGroupId != x.UserGroupId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();

            return hasResult;
        }

    }
}
