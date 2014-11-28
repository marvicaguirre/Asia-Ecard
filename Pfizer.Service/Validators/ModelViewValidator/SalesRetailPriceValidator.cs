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
    public class SalesRetailPriceValidator : AbstractViewModelValidator<ISalesRetailPriceViewModel>
    {
        public SalesRetailPriceValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            RuleFor(o => o.From)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HasNoDuplicate)
                .WithMessage("{PropertyName} must be unique.");
            RuleFor(o => o.Price)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty();
        }
        private bool HasNoDuplicate(ISalesRetailPriceViewModel x, DateTime? e)
        {
            //TODO: From Date should be unique per Product per Dosage Form. Right now it only checks with all items 
            var repository = UnitOfWork.Repository<SalesRetailPrice>();
            var hasResult = !repository.Query()
                .Filter(o => o.ProductId == x.ProductId)
                .Filter(o => o.Dosage.DosageId == x.DosageId)
                .Filter(o => o.From == e)
                .GetResult().Any();
            return hasResult;
        }

        //TODO: If the user entered a To Dat , To Date should be greater than or equal to From Date 
	    //TODO:Price should be greater than zero (0) 

    }
}
