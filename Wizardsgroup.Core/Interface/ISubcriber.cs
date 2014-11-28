namespace Wizardsgroup.Core.Interface
{
    public interface ISubscriber<TEventArg>
    {
        void OnEvent(object sender,TEventArg e);
    }
}