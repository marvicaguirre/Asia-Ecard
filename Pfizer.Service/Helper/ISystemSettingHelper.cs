namespace Pfizer.Service.Helper
{
    public interface ISystemSettingHelper
    {
        bool HasSetting(string settingName);
        dynamic GetSettingValue(string settingName);
    }
}