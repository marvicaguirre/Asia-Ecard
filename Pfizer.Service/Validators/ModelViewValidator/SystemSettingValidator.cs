using System;
using FluentValidation;
using Pfizer.Domain.Interfaces.ViewModel;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Service;

namespace Pfizer.Service.Validators.ModelViewValidator
{
    public class SystemSettingValidator : AbstractViewModelValidator<ISystemSetting>
    {

        

        public SystemSettingValidator(Func<IUnitOfWork> unitOfWorkFunction)
            : base(unitOfWorkFunction)
        {
            When(o => o.DataType == "int32", () =>
                {
                    RuleFor(o => o.SettingValue)
                       .Cascade(CascadeMode.StopOnFirstFailure)
                       .NotEmpty()
                       .Must((x, e) => IsCorrectType(x))
                       .WithMessage("{PropertyName}.");
                });

            When(o => o.DataType == "decimal", () =>
            {
                RuleFor(o => o.SettingValue)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .NotEmpty()
                   .Must((x, e) => IsCorrectType(x))
                   .WithMessage("{PropertyName} must be number or with decimal.");
            });

            When(o => o.DataType.ToLower().ToString() == "bit", () =>
            {
                RuleFor(o => o.SettingValue)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .NotEmpty()
                   .Must((x, e) => IsCorrectType(x))
                   .WithMessage("{PropertyName} must be True or False.");
            });
        }

        private bool IsCorrectType(ISystemSetting arg1)
        {
            
            if (arg1.DataType == "int32" && arg1.SettingValue != null)
            {
                int intResult;

                if (int.TryParse(arg1.SettingValue, out intResult))
                {
                    return true;
                }
            }

            else if (arg1.DataType == "decimal" && arg1.SettingValue != null)
            {
                decimal decResult;

                if (decimal.TryParse(arg1.SettingValue, out decResult))
                {
                    return true;
                }
            }

            else if (arg1.DataType == "bit" && arg1.SettingValue != null)
            {
                bool boolResult;
                    if (Boolean.TryParse(arg1.SettingValue,out boolResult))
	            {
                    return true;    
	            }
            }

            else if (arg1.DataType == "string" && arg1.SettingValue != null)
            {
                return true;
            }

           

            return false;
        }
        

        //private string message(string x)
        //{
        //    string ThisMessage = string.Empty;

        //    if (x == "decimal")
        //    {
        //        ThisMessage = "Dex";
        //    }

        //    return ThisMessage;
        //}
    }
}
