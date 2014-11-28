using System;

namespace Wizardsgroup.Utilities.Extensions
{
    internal class CustomIterator<T> : ICustomIterator<T>
    {
        private readonly T[] _items;

        public CustomIterator(T[] items)
        {
            _items = items;
        }

        public void ForEach(Action<T> action, Action<ILoopCondition<T>> loopCondition = null)
        {
            var condition = new LoopCondition<T>();
            var conditionAction = SetupConditionAction(loopCondition);
            conditionAction(condition);
            
            Action<T> nullAction = arg => { };

            foreach (T item in _items)
            {
                var actionToExecute = condition.Continue(item) ? nullAction : action;
                actionToExecute(item);
                if (condition.Exit(item)) break;
            }
        }

        private Action<ILoopCondition<T>> SetupConditionAction(Action<ILoopCondition<T>> loopCondition)
        {
            Action<ILoopCondition<T>> nullAction = arg => arg.ExitWhen(item => false).SkipWhen(item => false);
            Action<ILoopCondition<T>> conditionAction = loopCondition ?? nullAction;
            return conditionAction;
        }
    }
}