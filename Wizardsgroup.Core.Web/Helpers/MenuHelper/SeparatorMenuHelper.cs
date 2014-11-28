namespace Wizardsgroup.Core.Web.Helpers.MenuHelper
{
    public class SeparatorMenuHelper
    {
        public static MenuItem CreateSeparator()
        {
            return new MenuItem { IsMenuSeparator = true };
        }
    }
}