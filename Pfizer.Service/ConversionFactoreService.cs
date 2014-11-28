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
    public class ConversionFactoreService : AbstractEntityService<ConversionFactor>
    {
        public ConversionFactoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<ConversionFactor, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.ConversionFactorId.Equals(id);
        }

        protected override Expression<Func<ConversionFactor, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<ConversionFactor> OrderBy(IQueryable<ConversionFactor> arg)
        {
            return arg.OrderBy(o => o.PfizerCode);
        }
    }
}
