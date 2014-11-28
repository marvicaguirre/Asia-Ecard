using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Service
{
    public class DataDictionaryService : AbstractEntityService<DataDictionary>
    {
        public DataDictionaryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<DataDictionary, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.DataDictionaryId == id;
        }

        protected override Expression<Func<DataDictionary, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<DataDictionary> OrderBy(IQueryable<DataDictionary> arg)
        {
            return arg.OrderBy(o => o.Model).ThenBy(o => o.FieldName);
        }

        public IEnumerable<DataDictionary> GetDictionaryFromModel(string modelName)
        {
            return GetAll().Where(o => o.Model == modelName);
        }
    }
}
