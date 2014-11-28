using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Wizardsgroup.Domain.Attributes;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Repository.Context
{
    internal class DescriptionUpdater<TContext> 
        where TContext : DbContext
    {
        TContext context;
        Type contextType;
        DbTransaction transaction = null;

        public DescriptionUpdater(TContext context)
        {
            this.context = context;
        }
        
        private void SetTableDescriptions(Type tableType)
        {
            string fullTableName = context.GetTableName(tableType);

            if (fullTableName != string.Empty)
            {
                Regex regex = new Regex(@"(\[\w+\]\.)?\[(?<table>.*)\]");
                Match match = regex.Match(fullTableName);
                string tableName = string.Empty;
                var tableAttrs = tableType.GetCustomAttributes(typeof(TableAttribute), false);

                if (match.Success)
                    tableName = match.Groups["table"].Value;
                else
                    tableName = fullTableName;

                if (tableAttrs.Length > 0)
                    tableName = ((TableAttribute)tableAttrs[0]).Name;

                SetTableDescription(tableName, tableType);

                var properties = tableType.GetProperties(
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);
                
                foreach (var property in properties)
                {
                    //if (!property.GetType().GetProperties().Where(o => o.GetMethod.IsVirtual).Any())
                        SetColumnDescription(tableName, property);
                }
            }
        }

        private void SetColumnDescription(string tableName, PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(ColumnDescription), false);

            if (attributes.Length > 0)
            {
                var description = ((ColumnDescription)attributes[0]).Description;
                var sampleData = ((ColumnDescription)attributes[0]).SampleData;

                if (sampleData == null)
                    sampleData = string.Empty;

                SetColumnDescription(tableName, property.Name,
                    string.Format("{0}{1}",
                        description,
                        sampleData.Trim() != string.Empty ? " - Example: " + sampleData : string.Empty));
            }
        }

        private void SetTableDescription(string tableName, Type tableType)
        {
            if (tableType.CustomAttributes.Count() > 0)
            {
                var tableAttributes = tableType.GetCustomAttributes(typeof(ColumnDescription), false);

                if (tableAttributes.Length > 0)
                {
                    SetTableDescription(tableName, ((TableDescription)tableAttributes[0]).Description);
                }
            }
        }

        private void SetTableDescription(string tableName, string description)
        {
            string getDescription = "select [value] from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "','column',null) where objname = N'" + tableName + "';";
            var previousDescription = RunSqlScalar(getDescription);

            RunSql(
                string.Format(@"EXEC {0} @name = N'MS_Description', @value = @desc, 
@level0type = N'Schema', @level0name = 'dbo', 
@level1type = N'Table',  @level1name = @table;",
                    previousDescription != null ? "sp_updateextendedproperty" : "sp_addextendedproperty"),
                new SqlParameter("@table", tableName),
                new SqlParameter("@desc", description));
        }

        private void SetColumnDescription(string tableName, string columnName, string description)
        {
            string getDescription = "select [value] from fn_listextendedproperty('MS_Description','schema','dbo','table',N'" + tableName + "','column',null) where objname = N'" + columnName + "';";
            var previousDescription = RunSqlScalar(getDescription);

            RunSql(
                string.Format(@"EXEC {0} @name = N'MS_Description', @value = @desc, 
@level0type = N'Schema', @level0name = 'dbo', 
@level1type = N'Table',  @level1name = @table, 
@level2type = N'Column', @level2name = @column;",
                    previousDescription != null ? "sp_updateextendedproperty" : "sp_addextendedproperty"),
                new SqlParameter("@table", tableName),
                new SqlParameter("@column", columnName),
                new SqlParameter("@desc", description));
        }

        DbCommand CreateCommand(string commandText, params SqlParameter[] parameters)
        {
            var command = context.Database.Connection.CreateCommand();

            command.CommandText = commandText;
            command.Transaction = transaction;
            foreach (var p in parameters)
                command.Parameters.Add(p);

            return command;
        }

        void RunSql(string commandText, params SqlParameter[] parameters)
        {
            var command = CreateCommand(commandText, parameters);

            command.ExecuteNonQuery();
        }

        object RunSqlScalar(string commandText, params SqlParameter[] parameters)
        {
            var command = CreateCommand(commandText, parameters);

            return command.ExecuteScalar();
        }

        public void UpdateDescriptions()
        {
            contextType = typeof(TContext);
            var properties = contextType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            try
            {
                context.Database.Connection.Open();
                transaction = context.Database.Connection.BeginTransaction();
                foreach (var property in properties)
                {
                    if (property.PropertyType.InheritsOrImplements((typeof(DbSet<>))))
                    {
                        var tableType = property.PropertyType.GetGenericArguments()[0];

                        try
                        {
                            SetTableDescriptions(tableType);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(string.Format("[Context][DescriptionUpdater][SetTableDescriptions] {0}", ex.StackTrace));
                        }
                    }
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                Logger.Log(string.Format("[Context][DescriptionUpdater] {0}", ex.StackTrace));
            }
            finally
            {
                if (context.Database.Connection.State == System.Data.ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }
    }
}

public static class ReflectionUtil
{
    public static bool InheritsOrImplements(this Type child, Type parent)
    {
        bool inheritsOrImplements = false;

        parent = ResolveGenericTypeDefinition(parent);

        var currentChild = child.IsGenericType
                               ? child.GetGenericTypeDefinition()
                               : child;

        while (currentChild != typeof(object))
        {
            if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
            {
                inheritsOrImplements = true;
                break;
            }

            currentChild = currentChild.BaseType != null
                           && currentChild.BaseType.IsGenericType
                               ? currentChild.BaseType.GetGenericTypeDefinition()
                               : currentChild.BaseType;

            if (currentChild == null)
                inheritsOrImplements = false;
        }

        return inheritsOrImplements;
    }

    private static bool HasAnyInterfaces(Type parent, Type child)
    {
        return child.GetInterfaces()
            .Any(childInterface =>
            {
                var currentInterface = childInterface.IsGenericType
                    ? childInterface.GetGenericTypeDefinition()
                    : childInterface;

                return currentInterface == parent;
            });
    }

    private static Type ResolveGenericTypeDefinition(Type parent)
    {
        var shouldUseGenericType = true;

        if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
            shouldUseGenericType = false;

        if (parent.IsGenericType && shouldUseGenericType)
            parent = parent.GetGenericTypeDefinition();

        return parent;
    }
}

public static class ContextExtensions
{
    public static string GetTableName(this DbContext context, Type tableType)
    {
        MethodInfo method = typeof(ContextExtensions).GetMethod("GetTableName", new Type[] { typeof(DbContext) })
                         .MakeGenericMethod(new Type[] { tableType });
        return (string)method.Invoke(context, new object[] { context });
    }

    public static string GetTableName<T>(this DbContext context) where T : class
    {
        ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

        return objectContext.GetTableName<T>();
    }

    public static string GetTableName<T>(this ObjectContext context) where T : class
    {
        string table = string.Empty;

        try
        {
            string sql = context.CreateObjectSet<T>().OfType<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);

            table = match.Groups["table"].Value;
        }
        catch (Exception ex)
        {
            Logger.Log(string.Format("[Context][DescriptionUpdater] {0}", ex.StackTrace));
        }

        return table;
    }
}
