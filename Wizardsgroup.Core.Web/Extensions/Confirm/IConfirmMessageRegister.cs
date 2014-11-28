using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IConfirmMessageRegister<TModel>
    {
        IConfirmMessageItemRegister<TModel> For<TValue>(Expression<Func<TModel, TValue>> expression,ConfirmForType confirmForType);
    }
}