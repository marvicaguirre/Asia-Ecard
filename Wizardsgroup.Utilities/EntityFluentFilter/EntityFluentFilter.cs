using System;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Utilities.EntityFluentFilter
{
    public class EntityFluentFilter<T> : IFluentEntityFilter<T>
    {
        private readonly IEntityService<T> _service;        

        public EntityFluentFilter(IEntityService<T> service)
        {
            service.Guard("service must not be null.");
            _service = service;
        }

        public IEntityFilter<T> Filter(Expression<Func<T, bool>> filter)
        {
            return new EntityFilter<T>(_service, filter);
        }
    }
}
