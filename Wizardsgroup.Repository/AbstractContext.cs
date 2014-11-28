using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Wizardsgroup.Repository
{
    public abstract class AbstractContext : DbContext, IContext
    {

        protected Database DatabaseWrapper { get { return base.Database; } }

        #region Constructor
        protected AbstractContext(string connectionString)
            : base(connectionString)
        {            
        }
        #endregion

        #region IContext Implementation
        public IDbSet<T> EntitySet<T>() where T : class
        {
            return Set<T>();
        }

        public void SetAddState<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void SetModifiedState<T>(T entity) where T : class
        {
            try
            {
                Entry(entity).State = EntityState.Modified;
            }
            catch (InvalidOperationException)
            {
                var entry = Entry(entity);
                var key = GetPrimaryKey(entry);

                if (entry.State != EntityState.Detached) return;
                var currentEntry = EntitySet<T>().Find(key);

                if (currentEntry != null)
                {
                    var attachedEntry = Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    EntitySet<T>().Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void SetDeletedModified<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Deleted;
            ChangeTracker.DetectChanges();
        }
        #endregion

        #region Private Functions/Methods
        private Guid GetPrimaryKey(System.Data.Entity.Infrastructure.DbEntityEntry entry)
        {
            var myObject = entry.Entity;
            var property = myObject.GetType().GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
            return (Guid)property.GetValue(myObject, null);
        }
        #endregion
    }
}
