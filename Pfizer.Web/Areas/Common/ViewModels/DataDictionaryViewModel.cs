using System;
namespace Pfizer.Web.Areas.Common.ViewModels
{
    public class DataDictionaryViewModel
    {
        public Guid DataDictionaryId { get; set; }
        public string Model { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldDisplayText { get; set; }
    }
}