using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Domain.Lookup;
using Wizardsgroup.Service.Factories;
using Wizardsgroup.Service.Specification;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Service.Lookup
{
    public abstract class AbstractBaseLookup<T> : ILookupValueField, ILookupFunction where T : class,new()
    {        
        #region Members
        protected readonly IRepository<T> Repository;
        #endregion

        #region Properties
        public string Value
        {
            get
            {
                var memberExpression = (MemberExpression)GetValueFieldHelper().Body;
                return memberExpression.Member.Name;
            }
        }
        public string Text
        {
            get
            {
                var memberExpression = (MemberExpression)GetTextFieldHelper().Body;
                return memberExpression.Member.Name;
            }
        }
        public object Specification { get; set; }
        public string TextFilter { get; set; }

        internal ILookupService<T> LookupConverter
        {
            get
            {                
                return new LookupService<T>(new LookupDataFactory(ReflectionHelper.Instance),GetValueFieldHelper(),GetTextFieldHelper());
            }
        }
        #endregion

        #region Constructor
        protected AbstractBaseLookup(IUnitOfWork unitOfWork)
        {
            
            unitOfWork.Guard("IUnitOfWork must not be null");
            Repository = unitOfWork.Repository<T>();
        }    
        #endregion  

        #region Abstract Function/Methods
        protected abstract IEnumerable<T> GetCascadeResultHelper(int id);
        protected abstract Expression<Func<T, string>> GetTextFieldHelper();
        protected abstract Expression<Func<T, int>> GetValueFieldHelper();
        public abstract IEnumerable<ILookupValueField> GetRecordsForLookup();
        public abstract IEnumerable<ILookupValueField> GetRecordsForCascade(int id); 
        #endregion

        #region Virtual Functions/Methods
        protected ISpecification<T> ResolveSpecification()
        {
            var func = Specification as Func<ISpecification<T>> ?? (() => new NullSpecification<T>());
            var spefication = func();
            return spefication;
        }
        protected virtual IEnumerable<ILookupValueField> GetResult(Func<IEnumerable<T>> getFunction)
        {
            Func<T, string> adapt = GetTextFieldHelper().Compile();
            var records = getFunction().OrderBy(adapt).ToList();
            var result = LookupDataResult(records, LookupConverter.ConvertRecordToLookUp<LookupData>);
            return result;
        }

        protected virtual IQueryable<T> GetRecordsForLookupWorker()
        {
            var records = Repository.Query().FilterActive().GetResult();
            return records;
        }

        protected virtual IEnumerable<ILookupValueField> LookupDataResult(IEnumerable<T> records,
            Func<IEnumerable<T>, IEnumerable<ILookupValueField>> convertFunction)
        {
            records = OverridableDefaultOrder(records);
            var result = convertFunction(records);            
            return result;
        }

        protected virtual IEnumerable<T> OverridableDefaultOrder(IEnumerable<T> arg)
        {
            return arg;
        }

        protected Expression<Func<T, bool>> CreateContainsSearchExpression()
        {
            var expression = (MemberExpression)GetTextFieldHelper().Body;
            string name = expression.Member.Name;
            Expression<Func<T, bool>> searchFunction = GetContainsExpression(name, TextFilter);
            return searchFunction;
        }

        private Expression<Func<T, bool>> GetContainsExpression(string propertyName, string valueToCompare)
        {
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var constantExpression = Expression.Constant(valueToCompare, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, constantExpression);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }
        #endregion
    }
}
