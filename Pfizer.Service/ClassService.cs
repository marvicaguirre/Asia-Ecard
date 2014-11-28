using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;
using Wizardsgroup.Utilities.EventAggregator;

namespace Pfizer.Service
{
    public class ClassService : AbstractEntityService<Class>
    {
        #region Constructor
        public ClassService(IUnitOfWork unitOfWork)
            : this(unitOfWork, new SimpleEventAggregator())
        {
        }

        public ClassService(IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
            : base(unitOfWork, eventAggregator)
        {
        } 
        #endregion

        #region Functions/Methods
        protected override Expression<Func<Class, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.ClassId.Equals(id);
        }

        protected override System.Linq.Expressions.Expression<Func<Class, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<Class> OrderBy(IQueryable<Class> arg)
        {
            return arg.OrderBy(o => o.Name);
        } 
        #endregion
    }
}
