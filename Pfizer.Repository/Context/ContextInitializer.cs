using System.Data.Entity;
using Wizardsgroup.Repository;

namespace Pfizer.Repository.Context
{
    internal class ContextInitializer : IDatabaseInitializer<MainContext>
    {
        #region Implementation of IDatabaseInitializer<in SparkContext>

        public void InitializeDatabase(MainContext context)
        {
            new DescriptionUpdater<MainContext>(context).UpdateDescriptions();

            var initializer = new CommonContextInitializer();
            initializer.InitializeDatabase(context);
        }

        #endregion
    }
}
