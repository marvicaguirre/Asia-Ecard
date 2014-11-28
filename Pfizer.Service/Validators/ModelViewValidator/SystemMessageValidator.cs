using FluentValidation;
using System;
using Pfizer.Domain.Interfaces.ViewModel;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class SystemMessageValidator : AbstractViewModelValidator<ISystemMessageViewModel>
    {
        public SystemMessageValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.Message)
                            .NotEmpty();
        }
    }
}