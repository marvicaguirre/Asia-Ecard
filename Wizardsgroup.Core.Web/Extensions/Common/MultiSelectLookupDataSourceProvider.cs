using System;
using System.Linq.Expressions;
using Wizardsgroup.Service.Factories;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MultiSelectLookupDataSourceProvider : ILookupDataSource
    {
        internal Expression<Func<ILookupFactory>> LazyFactory { get; set; }
        internal string TargetLookup { get; set; }
        public ILookupDataSourceProvider Provider(Expression<Func<ILookupFactory>> lookupFactory)
        {
            LazyFactory = lookupFactory;
            return new LookupDataSourceProvider(this);
        }
    }
}