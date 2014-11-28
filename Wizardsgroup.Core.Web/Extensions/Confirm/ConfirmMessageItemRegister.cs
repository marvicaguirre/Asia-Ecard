using System.Linq;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ConfirmMessageItemRegister<TModel> : IConfirmMessageItemRegister<TModel>
    {
        private readonly ConfirmMessageContainer<TModel> _container;

        public ConfirmMessageItemRegister(ConfirmMessageContainer<TModel> container)
        {
            _container = container;
        }

        public IConfirmMessageSetup<TModel> CompareValueTo(string valueToCompare)
        {
            valueToCompare.Guard("Value to compare must not be null.");
            var lastItem = _container.Items.Last();
            lastItem.ValueToCompare = valueToCompare;
            return new ConfirmMessageObserverSetup<TModel>(_container);
        }
    }
}