using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Attributes;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Utilities.Interface;

namespace Wizardsgroup.Service.Factories
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IReflection _reflectionHelper;
        private readonly Func<IUnitOfWork> _unitOfWorkFunc;

        #region Constructor
        public ValidatorFactory(IReflection reflectionHelper, Func<IUnitOfWork> unitOfWorkFunc)
        {
            _reflectionHelper = reflectionHelper;
            _unitOfWorkFunc = unitOfWorkFunc;
        }

        #endregion

        #region Override for CreateInstance in ValidatorFactoryBase
        public override IValidator CreateInstance(Type validatorType)
        {
            var typeInGenericArgs = validatorType.GetGenericArguments();
            var validatorAttribute = (from type in typeInGenericArgs
                                      let attributeToCheck =
                                          type.GetCustomAttributes(typeof(ValidatorAttribute), true).FirstOrDefault()
                                      let validationAttr = attributeToCheck as ValidatorAttribute
                                      where validationAttr != null
                                      select validationAttr).FirstOrDefault();

            if (validatorAttribute != null)
            {
                var validatorTypeToCreate = validatorAttribute.ValidatorType;
                var validatorInstance = _reflectionHelper.CreateInstanceOfType<IValidator>(validatorTypeToCreate, _unitOfWorkFunc);
                return validatorInstance;
            }
            return null;
        }
        #endregion
    }
}
