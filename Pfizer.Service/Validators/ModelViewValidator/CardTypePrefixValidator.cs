using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class CardTypeValidator : AbstractViewModelValidator<ICardTypeViewModel>
    {
        public CardTypeValidator(Func<IUnitOfWork> unitOfWorkFunction) : base(unitOfWorkFunction)
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

        private bool HasNoDuplicate(ICardTypeViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<CardType>();
            var hasResult = !repository.Query()
                .Filter(o => o.CardTypeId != x.CardTypeId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();

            return hasResult;
        }
    }
}
