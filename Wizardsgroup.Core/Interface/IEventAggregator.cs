namespace Wizardsgroup.Core.Interface
{
    public interface IEventAggregator
    {
        void Subscribe(object subscriber);
        void Publish<TEventArg>(object sender, TEventArg eventToPublish);
    }
}