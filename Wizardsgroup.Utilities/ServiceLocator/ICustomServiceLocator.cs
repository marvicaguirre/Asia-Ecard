namespace Wizardsgroup.Utilities.ServiceLocator
{
    public interface ICustomServiceLocator
    {
        TService Resolve<TService>() where TService : class;
        bool HasRequiredService<TService>() where TService : class;
    }
}