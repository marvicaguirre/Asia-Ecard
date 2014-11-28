namespace Wizardsgroup.Utilities.ServiceLocator
{
    public interface IFluentServiceRegistrator
    {
        IServiceRegistrator<TService> For<TService>();
    }
}