using System;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Service.LinkedCascade
{
    public class LinkedCascadeData
    {
        #region Members

        private readonly IReflection _helper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _entityModelType;
        private readonly string _domainAssemblyName;        
        private const string RepositoryMethodName = "Repository";
        private const string RepositoryFindMethod = "Find";
        #endregion

        #region Constructor
        public LinkedCascadeData(IReflection helper, IUnitOfWork unitOfWork, string entityModelType,string domainAssemblyName)
        {
            helper.Guard("ReflectionHelper must not be null.");
            unitOfWork.Guard("UnitOfWork must nit be null.");
            entityModelType.Guard("TypeName must not be null or empty.");
            domainAssemblyName.Guard("DomainAssemblyName must not be null or empty.");

            _helper = helper;
            _unitOfWork = unitOfWork;
            _entityModelType = entityModelType;
            _domainAssemblyName = domainAssemblyName;
        }
        #endregion

        #region Public Methods/Function
        public object GetDataFromParent(int parentId, string propertyToFind)
        {
            var types = _helper.GetTypesFromAssembly(_domainAssemblyName);
            var entityType = _helper.GetType(types, _entityModelType);
            var repository = CreateRepositoryFromEntityType(entityType);
            var resultValue = QueryValueFromParent(repository, parentId, propertyToFind);
            return resultValue;
        }
        #endregion

        #region Private Methods/Function
        private object CreateRepositoryFromEntityType(Type entityModelType)
        {   //Creates repository of param type            
            var repositoryFactory = _helper.GetMethod<IUnitOfWork>(RepositoryMethodName);
            var genericMethod = _helper.MakeGenericMethod(repositoryFactory, entityModelType);
            var result = _helper.InvokeMethod(_unitOfWork, genericMethod, new object[] { });
            return result;
        }

        private object QueryValueFromParent(object repository, int parentId, string propertyToFind)
        {
            //Uses Find method in Repository with object param            
            var paramType = new[] { typeof(object) };
            var paramValues = new object[] { parentId };
            var repositoryResult = _helper.InvokeMethod(repository, RepositoryFindMethod, paramType, paramValues);
            if (repositoryResult == null)
                return null;
            var valueToReturn = _helper.GetPropetyValue<object>(repositoryResult, propertyToFind);
            return valueToReturn;
        }
        #endregion
    }
}
