using System;

namespace RuleEngine
{
    public class CompiledRule<T>
    {
        public Func<T, bool> Rule { get; set; }
        public string Field { get; set; }
        public string ValidationMessage { get; set; }
    }
}
