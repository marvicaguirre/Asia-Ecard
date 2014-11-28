using System.Collections.Generic;

namespace Wizardsgroup.Core.Web.Helpers.HomePageHelper
{
    public abstract class AbstractHomePage : IHomePageHelper
    {
        #region Implementation of IHomePageHelper

        public IEnumerable<HomePageItem> GetHomePageLinks()
        {
            return GetItemsWorker();
        }

        #endregion

        protected abstract IEnumerable<HomePageItem> GetItemsWorker();
    }
}