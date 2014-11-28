using System;
using System.Linq;
using System.Linq.Expressions;
using Pfizer.Domain.Models;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service
{
    public class ProductService : AbstractEntityService<Product>
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ProductService(IUnitOfWork unitOfWork,IEventAggregator eventAggregator) : base(unitOfWork, eventAggregator)
        {
            
        }

        protected override Expression<Func<Product, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.ProductId.Equals(id);
        }

        protected override Expression<Func<Product, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<Product> OrderBy(IQueryable<Product> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}