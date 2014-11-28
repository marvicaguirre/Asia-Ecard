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
    public class ProgramService : AbstractEntityService<Program>
    {
        public ProgramService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<Program, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.ProgramId.Equals(id);
        }

        protected override Expression<Func<Program, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<Program> OrderBy(IQueryable<Program> arg)
        {
            return arg.OrderBy(o => o.Name);
        }
    }
}
