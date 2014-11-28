using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Service.Specification
{
    [Serializable]
    public static class SpecificationFluentExtension
    {
        public static ISpecification<T> And<T>(this ISpecification<T> current, ISpecification<T> extension) where T : class ,new()
        {
            extension.IsSatisfiedBy = CombineSpecification(new Specification<T>
            {
                LeftNode = current,
                RightNode = extension,
                CombinedNodes = (left, right) => Expression.AndAlso(left, right),
            });
            return extension;
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> current, ISpecification<T> extension) where T : class ,new()
        {
            extension.IsSatisfiedBy = CombineSpecification(new Specification<T>
            {
                LeftNode = current,
                RightNode = extension,
                CombinedNodes = (left, right) => Expression.OrElse(left, right),
            });
            return extension;
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> current) where T : class ,new()
        {
            current.IsSatisfiedBy = CombineSpecification(new Specification<T>
            {
                LeftNode = current,
                RightNode = current,
                CombinedNodes = (left, right) => Expression.Not(left)
            });
            return current;
        }

        private class Specification<T> where T : class ,new()
        {
            public ISpecification<T> LeftNode { get; set; }
            public ISpecification<T> RightNode { get; set; }
            public Func<Expression, Expression, Expression> CombinedNodes { get; set; }
        }

        private static Expression<Func<T, bool>> CombineSpecification<T>(Specification<T> specification) where T : class ,new()
        {
            var visitor = new SpecificationVisitor(specification.LeftNode.IsSatisfiedBy.Parameters, specification.RightNode.IsSatisfiedBy.Parameters);
            var visitedNode = visitor.Visit(specification.LeftNode.IsSatisfiedBy.Body);
            var node = specification.CombinedNodes(visitedNode, specification.RightNode.IsSatisfiedBy.Body);
            var expression = Expression.Lambda<Func<T, bool>>(node, specification.RightNode.IsSatisfiedBy.Parameters[0]);
            return expression;
        }
    }
}
