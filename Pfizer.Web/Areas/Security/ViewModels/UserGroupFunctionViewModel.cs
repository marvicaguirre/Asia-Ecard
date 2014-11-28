using System;

namespace Pfizer.Web.Areas.Security.ViewModels
{
    public class UserGroupFunctionViewModel
    {
        public int UserGroupFunctionId { get; set; }
        public string FunctionName { get; set; }
        public string ModuleName { get; set; }

        public int? CentralFunctionId { get; set; }
        public int? UserGroupId { get; set; }
    }
}