using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RuleEngine.Enums;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Models;

namespace Wizardsgroup.Service
{
    public class RuleService : AbstractEntityService<RuleDatastore>
    {
        public RuleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override Expression<Func<RuleDatastore, bool>> FindEntityPrimaryById(int id)
        {
            return o => o.RuleDatastoreId == id;
        }

        protected override Expression<Func<RuleDatastore, object>>[] Include()
        {
            return null;
        }

        protected override IOrderedQueryable<RuleDatastore> OrderBy(IQueryable<RuleDatastore> arg)
        {
            return arg.OrderBy(o => o.Controller).ThenBy(o=>o.ControllerAction).ThenBy(o=>o.Field);
        }

        public List<dynamic> GetAvailableRules()
        {
            var rules = new List<dynamic>
                {
                    new { Value = RuleOperator.Equal.ToString(),Text = RuleOperator.Equal.ToString() },
                    new { Value = RuleOperator.NotEqual.ToString(),Text = RuleOperator.NotEqual.ToString() },
                    new { Value = RuleOperator.GreaterThan.ToString(),Text = RuleOperator.GreaterThan.ToString() },
                    new { Value = RuleOperator.GreaterThanOrEqual.ToString(),Text = RuleOperator.GreaterThanOrEqual.ToString() },
                    new { Value = RuleOperator.LessThan.ToString(),Text = RuleOperator.LessThan.ToString() },
                    new { Value = RuleOperator.LessThanOrEqual.ToString(),Text = RuleOperator.LessThanOrEqual.ToString() },                    
                };
            return rules;
        }
        
    }
}
