using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Repository;
using Wizardsgroup.Utilities.Extensions;
using Wizardsgroup.Utilities.Interface;

namespace Pfizer.Repository.History
{
    internal class HistoryFactory
    {
        private readonly IReflection _reflectionHelper;
        private readonly string _assemblyName;

        public HistoryFactory(IReflection reflectionHelper, string assemblyName)
        {
            reflectionHelper.Guard("IReflection should not be null");
            _reflectionHelper = reflectionHelper;
            _assemblyName = assemblyName;
        }

        public List<IHistoryInserter> CreateHistoryInserter(IContext context)
        {
            var assemblyTypes = _reflectionHelper.GetTypesFromAssembly(_assemblyName);
            var historyInserter = _reflectionHelper.GetTypesWithImplementingInterface<IHistoryInserter>(assemblyTypes);
            return historyInserter.Select(o => CreateIntanceOfHistoryInserter(o, context)).ToList();
        }

        private IHistoryInserter CreateIntanceOfHistoryInserter(Type historyInserterType,IContext context)
        {
            var historyInserter = _reflectionHelper.CreateInstanceOfType<IHistoryInserter>(historyInserterType, context);
            return historyInserter;
        }
    }
}
