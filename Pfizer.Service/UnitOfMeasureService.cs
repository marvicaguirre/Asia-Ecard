using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class UnitOfMeasureService : AbstractEntityService<UnitOfMeasure>
    {
        public UnitOfMeasureService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<UnitOfMeasure, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.UnitOfMeasureId.Equals(id);
        }

        protected override Expression<Func<UnitOfMeasure, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<UnitOfMeasure> OrderBy(IQueryable<UnitOfMeasure> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}
