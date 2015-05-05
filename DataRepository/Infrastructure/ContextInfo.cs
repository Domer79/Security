using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

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
            _entityMetadataCollection = new EntityMetadataCollection(contextType);
        }

        public EntityMetadataCollection EntityMetadataCollection
        {
            get { return _entityMetadataCollection; }
        }

        internal string DatabaseName { get; set; }

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

        private static ContextInfoCollection _contextInfoCollection;

        public static ContextInfoCollection ContextInfoCollection
        {
            get { return _contextInfoCollection ?? (_contextInfoCollection = new ContextInfoCollection()); }
        }
    }
}