using System;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CustomActionDataSource: ICustomActionDataSource
    {
        public CustomActionDataSourceConfigContainer Configuration { get; private set; }

        public CustomActionDataSource()
        {
            Configuration = new CustomActionDataSourceConfigContainer();
        }

        public ICustomActionDataSource Read(Action<IReadAction> readAction)
        {
            readAction.Guard("ReadAction must not be null.");
            Configuration.ReadAction = readAction;
            Configuration.Url = null;
            Configuration.Parameter = null;
            return this;
        }

        public ICustomActionDataSource Url(string url,params string[] paramControls)
        {
            url.Guard("Url must not be null or empty.");
            Configuration.Url = url;
            Configuration.Parameter = paramControls;
            Configuration.ReadAction = null;
            return this;
        }
    }
}