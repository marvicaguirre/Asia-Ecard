using System.Linq;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ConfirmMessageObserverSetup<TModel> : IConfirmMessageSetup<TModel>
    {
        private readonly ConfirmMessageContainer<TModel> _container;

        public ConfirmMessageObserverSetup(ConfirmMessageContainer<TModel> container)
        {
            _container = container;
        }

        public IConfirmMessageRegister<TModel> Message(string message)
        {
            message.Guard("Message must not be null.");
            var lastItem = _container.Items.Last();
            lastItem.Message = message;
            return new ConfirmMessageRegister<TModel>(_container);
        }
    }
}