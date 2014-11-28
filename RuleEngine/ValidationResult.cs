using System.Collections.Generic;

namespace RuleEngine
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            if (ValidationDetails == null)
                ValidationDetails = new List<ValidationDetail>();
        }
        public string ValidationMessageSummary { get; set; }
        public bool Passed { get; set; }
        public List<ValidationDetail> ValidationDetails { get; set; }
    }
}
