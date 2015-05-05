using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace DataRepository.Infrastructure
{
    static internal class Tools
    {
        public static string GetTableName<T>(DbContext context) where T : ModelBase
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return GetTableName<T>(objectContext);
        }

        public static string GetTableName<T>(ObjectContext context) where T : ModelBase
        {
            var sql = context.CreateObjectSet<T>().ToTraceString();
            return GetTableNameFromSqlQuery(sql);
        }

        public static string GetTableNameFromSqlQuery(string sql)
        {
            var regex = new Regex(@"FROM\s+(?<table>.+)\s+AS", RegexOptions.IgnoreCase);
            var match = regex.Match(sql);

            var table = match.Groups["table"].Value;
            return table;
        }

        public static string GetDatabaseNameFromConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) 
                throw new ArgumentNullException("connectionString");

            var regex = new Regex(@"(?i)(initial catalog|database)\s*=\s*(?<databaseName>[\w]+)");
            var match = regex.Match(connectionString);

            if (!match.Success)
                return null;

            return match.Groups["databaseName"].Value;
        }
    }
}