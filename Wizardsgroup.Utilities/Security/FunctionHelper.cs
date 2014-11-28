using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Wizardsgroup.Utilities.Security
{
    public class FunctionHelper : IFunctionHelper
    {
        private readonly IReadOnlyCollection<IModuleFunctionContainer> _functionCollectionContainer;

        public FunctionHelper(IReadOnlyCollection<IModuleFunctionContainer> functionCollectionContainer)
        {
            functionCollectionContainer.Guard("IModuleFunctionCollectionContainer must not be null.");
            _functionCollectionContainer = functionCollectionContainer;
        }

        public string GetModuleFromFunctionName(string functionName)
        {
            var result = GetSecurityInfoFromFunctionName(functionName);
            if (result == null) return string.Empty;
            return result.FullModuleName;
        }

        public SecurityFunctionInfo GetSecurityInfoFromFunctionName(string functionName)
        {
            //special handling of logoff
            if (functionName.ToLower() == "logoff") return new SecurityFunctionInfo
                {
                    FunctionName = "Logoff",
                    FullModuleName = "Security",
                    MainGroupName = "Security",
                    SubGroupName = string.Empty,
                    FunctionShortName = "Logoff",                    
                };

            var result = GetFunctionNames().FirstOrDefault(o => o.FunctionName.ToLower().Equals(functionName.ToLower()));
            if (result != null)
            {
                return result;
            }
            Log(string.Format("'{0}' is missing from the list of known system functions. This needs to be added! Please go to Domain.Infrastructure and add entries to registrator.cs file", functionName));
            return new SecurityFunctionInfo();
        }

        public IReadOnlyCollection<SecurityFunctionInfo> GetFunctionNames()
        {
            return ConvertToSecurityInfo();
        }

        private List<SecurityFunctionInfo> ConvertToSecurityInfo()
        {
            const string space = " ";
            const string blank = "";
            const string dash = "-";
            var securityInfos = new List<SecurityFunctionInfo>();

            _functionCollectionContainer.ToList().ForEach(module =>
                {                      
                    var trimmedModuleName = module.GroupName.Replace(space,blank);
                    var spacedModuleName = module.GroupName.SplitCamelCase();          
                    var trimmedController = module.ModuleName.Replace(space,blank);
                    var spacedController = module.ModuleName.SplitCamelCase();

                    module.Functions.ForEach(moduleFunction =>
                        {
                            var securityInfo = new SecurityFunctionInfo
                                {
                                    FunctionName = string.Format("{0}{1}", moduleFunction, trimmedController),
                                    FunctionShortName = string.Format("{0}{1}", moduleFunction, trimmedController),
                                    FullModuleName = string.Format("{0}{1}{2}", trimmedModuleName, dash, trimmedController),
                                    FullModuleDisplayName = string.Format("{0}{1}{2}{1}{3}", spacedModuleName, space, dash, spacedController),
                                    FunctionDisplayName = string.Format("{0}{1}{2}", moduleFunction, space, spacedController),
                                    MainGroupName = module.GroupName,
                                    SubGroupName = module.SubgroupName,
                                };
                            securityInfos.Add(securityInfo);
                        });
                });
            return securityInfos;
        }

        private void Log(string message)
        {

            var finalMessage = string.Format("{0} : {1}{2}", DateTime.Now.ToLongTimeString(), message, Environment.NewLine);
            Logger.Log(finalMessage);
        }
    }
}