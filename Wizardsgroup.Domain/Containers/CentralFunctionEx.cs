using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wizardsgroup.Domain.Containers
{
    public class CentralFunctionEx
    {
        public int CentralFunctionId { get; set; }
        public string FullFunctionName { get; set; }
        public string FunctionName { get; set; }
        public string ModuleName { get; set; }
        [NotMapped]
        public int UserId { get; set; }
    }
}