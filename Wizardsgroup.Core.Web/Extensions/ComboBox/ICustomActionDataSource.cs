using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ICustomActionDataSource
    {
        ICustomActionDataSource Read(Action<IReadAction> readAction);
        ICustomActionDataSource Url(string url,params string[] paramControls);
    }
}