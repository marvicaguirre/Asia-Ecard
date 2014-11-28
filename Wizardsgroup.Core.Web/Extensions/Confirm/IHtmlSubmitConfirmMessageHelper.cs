using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlSubmitConfirmMessageHelper<TModel>
    {
        MvcHtmlString Register(Action<IConfirmMessageRegister<TModel>> register);
    }
}
