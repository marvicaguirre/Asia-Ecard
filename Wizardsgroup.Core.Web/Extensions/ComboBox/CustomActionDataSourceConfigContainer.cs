using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CustomActionDataSourceConfigContainer
    {
        public Action<IReadAction> ReadAction { get; set; }
        public string Url { get; set; }
        public string[] Parameter { get; set; } 
    }
}