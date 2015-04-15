using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using SystemTools.Extensions;

namespace DataRepository.Infrastructure
{
    public class ContextInfo
    {
        private readonly DbContext _context;

        public ContextInfo(DbContext context)
        {
            _context = context;
        }

        public string[] GetTableNames()
        {
            return GetContextEntities(_context).Select(GetTableName).ToArray();
        }

        public string GetTableName(Type type)
        {
            if (type == null && !type.Is<ModelBase>())
                throw new ArgumentNullException("type");

            var sqlQuery = _context.Set(type).AsNoTracking().ToString();
            return Tools.GetTableNameFromSqlQuery(sqlQuery);
        }

        public static IEnumerable<PropertyInfo> GetDbSetProperties(DbContext context)
        {
            var enumerable = context
                    .GetType()
                    .GetProperties()
                    .Where(CheckPropertyToDbSet);

            return enumerable;
        }

        public static IEnumerable<Type> GetContextEntities(DbContext context)
        {
            return GetDbSetProperties(context).Select(pi => pi.PropertyType.GetGenericArguments()[0]);
        }

        private static bool CheckPropertyToDbSet(PropertyInfo pi)
        {
            var checkPropertyToDbSet = pi.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDbSet<>));
            return checkPropertyToDbSet;
        }
    }
}