using System;
using FluentValidation;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Pfizer.Domain.Interfaces.ViewModel;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class LoginValidator : AbstractViewModelValidator<ILogin>
    {

        public LoginValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {

            RuleFor(o => o.UserName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                ;

            RuleFor(o => o.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                ;
        }        
    }
}
