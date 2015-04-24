using System;
using System.Collections;
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
        private readonly EntityMetadataCollection _entityMetadataCollection;

        public ContextInfo(DbContext context)
        {
            _context = context;
            _entityMetadataCollection = new EntityMetadataCollection(GetContextEntities(_context).ToArray());
        }

        public EntityMetadataCollection EntityMetadataCollection
        {
            get { return _entityMetadataCollection; }
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

        private static IEnumerable<Type> GetContextEntities(DbContext context)
        {
            return GetDbSetProperties(context).Select(pi => pi.PropertyType.GetGenericArguments()[0]);
        }

        private static bool CheckPropertyToDbSet(PropertyInfo pi)
        {
            var checkPropertyToDbSet = pi.PropertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDbSet<>));
            return checkPropertyToDbSet;
        }

        private static ContextInfoColelction _contextInfoColelction;

        public static ContextInfoColelction ContextInfoCollection
        {
            get { return _contextInfoColelction ?? (_contextInfoColelction = new ContextInfoColelction()); }
        }
    }

    public class ContextInfoColelction : IEnumerable<ContextInfo>
    {
        private readonly Dictionary<DbContext, ContextInfo> _dictionary = new Dictionary<DbContext, ContextInfo>();

        public void Add(DbContext context)
        {
            _dictionary.Add(context, new ContextInfo(context));
        }

        public ContextInfo this[DbContext context]
        {
            get { return _dictionary[context]; }
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<ContextInfo> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель, осуществляющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Collections.IEnumerator"/>, который может использоваться для перебора коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}