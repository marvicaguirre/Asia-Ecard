using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ConfirmMessageRegister<TModel> : IConfirmMessageRegister<TModel>
    {
        private readonly ConfirmMessageContainer<TModel> _container;
        public ConfirmMessageRegister(ConfirmMessageContainer<TModel> container)
        {
            _container = container;
        }

        public IConfirmMessageItemRegister<TModel> For<TValue>(Expression<Func<TModel, TValue>> expression, ConfirmForType confirmForType)
        {
            expression.Guard("Expression must not be null.");
            var modelMetaData = ModelMetadata.FromLambdaExpression(expression, _container.HtmlHelper.ViewData);
            _container.Items.Add(new ConfirmMessageItem
            {
                Metadata = modelMetaData,
            });
            return new ConfirmMessageItemRegister<TModel>(_container);
        }
    }
}