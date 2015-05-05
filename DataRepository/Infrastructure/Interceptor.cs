using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using SystemTools;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using DataRepository.Exceptions;

namespace DataRepository.Infrastructure
{
    public class Interceptor : IDbCommandInterceptor
    {
        public Interceptor()
        {
            Debug.WriteLine("Create new Interceptor");
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
//            throw new NotImplementedException();
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
//            throw new NotImplementedException();
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            var databaseName = Tools.GetDatabaseNameFromConnectionString(command.Connection.ConnectionString);
            var tableName = Tools.GetTableNameFromSqlQuery(command.CommandText);

            var contextInfo = ContextInfo.ContextInfoCollection.First(ci => ci.DatabaseName == databaseName);
            var entityMetadata = contextInfo.EntityMetadataCollection[tableName];
            
            if (entityMetadata == null)
                return;

            if (ApplicationCustomizer.Security == null)
                return;

            if (!ApplicationCustomizer.Security.IsAccess(entityMetadata.EntityAlias, ApplicationCustomizer.Security.Identity.Name, SecurityAccessType.Select))
                throw new EntityAccessDenied(entityMetadata);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
//            throw new NotImplementedException();
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
//            throw new NotImplementedException();
        }
    }
}
