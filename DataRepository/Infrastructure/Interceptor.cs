using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Security;
using SystemTools;
using SystemTools.Exceptions;
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
            if (!ApplicationCustomizer.EnableSecurity)
                return;

            if (ApplicationCustomizer.Security == null)
                throw new SecurityException2();

            var databaseName = Tools.GetDatabaseNameFromConnectionString(command.Connection.ConnectionString);
            var tableName = Tools.GetTableNameFromSqlQuery(command.CommandText);

            var contextInfo = ContextInfo.ContextInfoCollection.First(ci => ci.DatabaseName == databaseName);
            var entityMetadata = contextInfo.EntityMetadataCollection[tableName];
            
            if (entityMetadata == null)
                return;

            if (entityMetadata.AuthorizeSkip)
                return;

            if (ApplicationCustomizer.Security.Principal == null)
                throw new EntityAccessDeniedException(entityMetadata);

            if (!ApplicationCustomizer.Security.IsAccess(entityMetadata.EntityAlias, ApplicationCustomizer.Security.Principal.Identity.Name, SecurityAccessType.Select))
                throw new EntityAccessDeniedException(entityMetadata);
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
