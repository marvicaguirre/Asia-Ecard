using System;
using System.Linq;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class CardTypePrefixValidator : AbstractViewModelValidator<ICardPrefixViewModel>
    {
        public CardTypePrefixValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicate)
               .WithMessage("{PropertyName} must be unique.");
        }

        private bool HasNoDuplicate(ICardPrefixViewModel x, String e)
        {
            var repository = UnitOfWork.Repository<CardPrefix>();
            var hasResult = !repository.Query()
                .Filter(o => o.CardPrefixId != x.CardPrefixId)
                .Filter(o => o.Name == e.Trim())
                .GetResult().Any();

            return hasResult;
        }
    }
}
