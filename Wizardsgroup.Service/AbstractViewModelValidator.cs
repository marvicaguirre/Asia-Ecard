using System;
using FluentValidation;
using Wizardsgroup.Core.Interface;

namespace Wizardsgroup.Service
{
    public abstract class AbstractViewModelValidator<TViewModel> : AbstractValidator<TViewModel>
    {
        protected const string RuleForPropetyName = "{PropertyName}";
        private readonly Func<IUnitOfWork> _unitOfWorkFunction;
        private IUnitOfWork _unitOfWork;
        protected IUnitOfWork UnitOfWork { get { return LazyLoadedContext(); } }
        ////UnitOfWork is lazy loaded, thats why the unitofwork is injected as func expression
        ////force concrete class to implement injection of unitofwork
        protected AbstractViewModelValidator(Func<IUnitOfWork> unitOfWorkFunction)
        {
            _unitOfWorkFunction = unitOfWorkFunction;            
        }

        private IUnitOfWork LazyLoadedContext()
        {
            return _unitOfWork ?? (_unitOfWork = _unitOfWorkFunction());
        }
    }
}
