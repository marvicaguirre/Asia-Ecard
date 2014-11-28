using System;
using System.Linq.Expressions;
using Wizardsgroup.Service.Factories;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ILookupDataSource
    {
        ILookupDataSourceProvider Provider(Expression<Func<ILookupFactory>> lookupFactory);        
    }
}