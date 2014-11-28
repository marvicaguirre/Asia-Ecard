using System;
namespace Pfizer.Web.Areas.Common.ViewModels
{
    public class RuleDatastoreViewModel
    {
        public Guid RuleDatastoreId { get; set; }
        public string Controller { get; set; }
        public string ControllerAction { get; set; }
        public string Field { get; set; }
        public string RuleOperator { get; set; }
        public string Value { get; set; }
        public string ValidationMessage { get; set; }
    }
}