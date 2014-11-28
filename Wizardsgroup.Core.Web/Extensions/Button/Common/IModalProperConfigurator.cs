namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IModalProperConfigurator
    {        
        IModalProperConfigurator Title(string title);
        IModalProperConfigurator Width(int? width);
        IModalProperConfigurator Height(int? height);
        IModalProperConfigurator AutoClose(bool autoClose = true);
        IModalProperConfigurator ConfirmOnClose(bool confirm = true);
    }
}