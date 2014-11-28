using System;
using System.Linq;
using System.Linq.Expressions;
using RuleEngine.Enums;
using RuleEngine.Interfaces;

namespace RuleEngine.Builders
{
    internal class ConditionalRuleBuilder<T> : IRuleBuilder
    {
        #region Members
        private readonly object _valueOrLeftNode;
        private readonly RuleOperator _ruleOperator;
        private readonly object _valueOrRightNode;
        private readonly ParameterExpression _parameterExpression;
        #endregion

        #region Constructor
        public ConditionalRuleBuilder(IRule rule, ParameterExpression parameterExpression)
        {
            _valueOrLeftNode = rule.LeftNode;
            _ruleOperator = rule.Operator;
            _valueOrRightNode = rule.RightNode;
            _parameterExpression = parameterExpression;
        }
        #endregion

        #region Public Method/Function

        public Expression Build()
        {            
            var expressionTypeValue = _GetExpressionFromOperatorEnumerate(_ruleOperator == RuleOperator.None ? RuleOperator.Equal :_ruleOperator);
            var leftNode = _CreateExpressionFromValueOrNode(_ruleOperator == RuleOperator.None ? true : _valueOrLeftNode, _parameterExpression);
            var rightNode = _CreateExpressionFromValueOrNode(_ruleOperator == RuleOperator.None ? true : _valueOrRightNode, _parameterExpression, leftNode.Type);            
            return _BuildExpressionFromExpression(expressionTypeValue, leftNode, rightNode);
        }
        #endregion

        #region Private Method/Function
        private Expression _CreateExpressionFromValueOrNode(object valueOrNodeName, ParameterExpression parameterExpression,Type type = null)
        {
            Expression expression;
            var value = valueOrNodeName ?? new object();
            var belongsToClassOfType = _PropertyBelongsToClassOfType(value);

            //TODO Refactor this!!!!
            if (belongsToClassOfType)
            {
                expression = Expression.Property(parameterExpression, value.ToString());
            }
            else if (valueOrNodeName == null)
            {
                value = _GetDefaultValueFromType(type);
                expression = Expression.Constant(IsNullableType(type) ? null : Convert.ChangeType(value, type));
            }
            else if (type != null && _CanChangeSourceTypeToTargetType(value, type))
            {
                expression = Expression.Constant(Convert.ChangeType(value, type));
            }
            else
            {
                expression = Expression.Constant(Convert.ChangeType(value, value.GetType()));
            }

            return expression;
        }

        private static bool IsNullableType(Type type)
        {
            return type == null || type.GenericTypeArguments.Any();
        }

        private object _GetDefaultValueFromType(Type type)
        {
            return type != null ? (type.IsValueType ? Activator.CreateInstance(type) : null) : null;
        }

        private bool _PropertyBelongsToClassOfType(object objectToInspect)
        {
            if (objectToInspect == null)
                return false;
            try
            {
                var propertyInfo = typeof(T).GetProperty(objectToInspect.ToString());                
                return propertyInfo != null;
            }
            catch
            {
                return false;
            }
        }

        private ExpressionType _GetExpressionFromOperatorEnumerate(RuleOperator ruleOperator)
        {
            const ExpressionType expressionType = new ExpressionType();
            var fieldInfo = expressionType.GetType().GetField(Enum.GetName(typeof(RuleOperator), ruleOperator));
            return (ExpressionType)fieldInfo.GetValue(ruleOperator);
        }

        private Expression _BuildExpressionFromExpression(ExpressionType expressionTypeValue, Expression leftOperand, Expression rightOperand)
        {
            Expression expression = null;
            if (leftOperand.Type == rightOperand.Type)
                expression = Expression.MakeBinary(expressionTypeValue, leftOperand, rightOperand);
            else if (_CanChangeSourceTypeToTargetType(rightOperand, leftOperand.Type))
            {
                if (rightOperand.Type != typeof(bool))
                {
                    rightOperand = Expression.Constant(Convert.ChangeType(rightOperand, leftOperand.Type));
                }
                else
                {
                    leftOperand = Expression.Constant(Convert.ChangeType(rightOperand, rightOperand.Type));
                }                
                expression = Expression.MakeBinary(expressionTypeValue, leftOperand, rightOperand);
            }
            else            
                expression = Expression.MakeBinary(expressionTypeValue, leftOperand, rightOperand);            

            return expression == null ? null : Expression.Lambda<Func<T, bool>>(expression, _parameterExpression);
        }

        private bool _CanChangeSourceTypeToTargetType(object sourceType, Type targetType)
        {
            try
            {
                var returnValue = Convert.ChangeType(sourceType, targetType);
                return returnValue != null;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
