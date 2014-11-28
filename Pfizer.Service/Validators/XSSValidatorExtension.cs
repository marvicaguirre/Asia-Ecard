using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Pfizer.Service.Validators
{
    public static class XSSValidatorExtension
    {
        public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {            
            ruleBuilder.Must((x,y)=>
                {
                    if (y == null)
                        return true;

                    var specialChar = new List<char> {'<', '>','&','"'};
                    var hasResult = specialChar.Any(c => y.ToString().Contains(c));
                    return !hasResult;
                }).WithMessage("'{PropertyName}' should not have special character.");
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder);            
        }
    }
}
