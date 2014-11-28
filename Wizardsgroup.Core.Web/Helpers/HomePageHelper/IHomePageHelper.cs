using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Helpers.HomePageHelper
{
    public interface IHomePageHelper
    {
        IEnumerable<HomePageItem> GetHomePageLinks();
    }
}