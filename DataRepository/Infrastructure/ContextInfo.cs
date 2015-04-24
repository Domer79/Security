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
        private readonly EntityMetadataCollection _entityMetadataCollection;

        public ContextInfo(DbContext context)
            : this(context.GetType())
        {
            
        }
        public ContextInfo(Type contextType)
        {
            _entityMetadataCollection = new EntityMetadataCollection(GetContextEntities(contextType).ToArray());
        }

        public EntityMetadataCollection EntityMetadataCollection
        {
            get { return _entityMetadataCollection; }
        }

        public static IEnumerable<PropertyInfo> GetDbSetProperties(Type contextType)
        {
            var enumerable = contextType
                    .GetProperties()
                    .Where(CheckPropertyToDbSet);

            return enumerable;
        }

        internal static IEnumerable<Type> GetContextEntities(Type contextType)
        {
            return GetDbSetProperties(contextType).Select(pi => pi.PropertyType.GetGenericArguments()[0]);
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
        private readonly Dictionary<string, ContextInfo> _dictionary = new Dictionary<string, ContextInfo>();

        public void Add(Type contextType)
        {
            if (contextType == null || !contextType.Is<DbContext>()) 
                throw new ArgumentNullException("contextType");

            if (_dictionary.ContainsKey(contextType.FullName))
                return;

            _dictionary.Add(contextType.FullName, new ContextInfo(contextType));
        }

        public ContextInfo this[Type contextType]
        {
            get
            {
                if (contextType == null) 
                    throw new ArgumentNullException("contextType");

                return _dictionary[contextType.FullName];
            }
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