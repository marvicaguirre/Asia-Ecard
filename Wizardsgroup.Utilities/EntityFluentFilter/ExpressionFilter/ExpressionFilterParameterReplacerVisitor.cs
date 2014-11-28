using System.Collections.Generic;
using System.Linq.Expressions;

namespace Wizardsgroup.Utilities.EntityFluentFilter.ExpressionFilter
{
    internal class ExpressionFilterParameterReplacerVisitor : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, ParameterExpression> _parameterReplacements;

        public ExpressionFilterParameterReplacerVisitor(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
        {
            _parameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();
            for (var i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
                _parameterReplacements.Add(fromParameters[i], toParameters[i]);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            ParameterExpression replacement;
            if (_parameterReplacements.TryGetValue(node, out replacement))
                node = replacement;
            return base.VisitParameter(node);
        }
    }
}
