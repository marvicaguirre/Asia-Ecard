using System;

namespace Wizardsgroup.Utilities.Extensions
{
    internal class LoopCondition<T> : ILoopCondition<T>
    {
        public Func<T, bool> Exit { get; private set; }
        public Func<T, bool> Continue { get; private set; }
        public LoopCondition()
        {

            Exit = arg => false;
            Continue = arg => false;
        }
        public ILoopCondition<T> ExitWhen(Func<T, bool> exit)
        {
            exit.Guard("Exit delegate must not be null.");
            Exit = exit;
            return this;
        }

        public ILoopCondition<T> SkipWhen(Func<T, bool> skip)
        {
            skip.Guard("Skip delegate must not be null");
            Continue = skip;
            return this;
        }
    }
}