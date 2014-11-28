namespace Wizardsgroup.Utilities.Extensions
{
    public interface IEnumGuard<T>
    {
        IEnumGuard<T> CheckAndThrowNull();
        IEnumGuard<T> CheckAndThrowInvalidEnumArgument();
        IEnumGuard<T> CheckAndThrowNotEnum();
    }
}