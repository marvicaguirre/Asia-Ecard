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
    public class SalesRetailPriceService : AbstractEntityService<SalesRetailPrice>
    {
        public SalesRetailPriceService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<SalesRetailPrice, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.SalesRetailPriceId.Equals(id);
        }

        protected override Expression<Func<SalesRetailPrice, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<SalesRetailPrice> OrderBy(IQueryable<SalesRetailPrice> arg)
        {
            return arg.OrderBy(o => o.From);
        }
    }
}
