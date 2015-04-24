using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using SystemTools.WebTools.Attributes;

namespace DataRepository.Infrastructure
{
    public class EntityInfo<TEntity> where TEntity : ModelBase
    {
        private readonly DbContext _context;
        private string _keyName;
        private EntityMetadata _entityMetadata;

        public EntityInfo(DbContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _context = context;
            _entityMetadata = new EntityMetadata(typeof(TEntity));
        }

        private ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter) _context).ObjectContext; }
        }

        public string KeyName
        {
            get { return _keyName ?? (_keyName = GetEntityKeyName()); }
        }

        string GetEntityKeyName()
        {
            var set = ObjectContext.CreateObjectSet<TEntity>();
            var entitySet = set.EntitySet;
            return entitySet.ElementType.KeyMembers[0].Name;
        }

        public Expression<Func<TEntity, TKey>> GetMemberAccess<TKey>()
        {
            return GetMemberAccess<TKey>(null);
        }

        public Expression<Func<TEntity, TKey>> GetMemberAccess<TKey>(string columnName)
        {
            var parameter = Expression.Parameter(typeof (TEntity), "p");
            var memberAccess = Expression.Property(parameter, string.IsNullOrEmpty(columnName) ? KeyName : columnName);
            return (Expression<Func<TEntity, TKey>>)Expression.Lambda(memberAccess, parameter);
        }


        public Expression<Func<TEntity, object>> GetMemberAccess(string columnName)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "p");
            var memberAccess = Expression.Property(parameter, string.IsNullOrEmpty(columnName) ? KeyName : columnName);
            return (Expression<Func<TEntity, object>>)Expression.Lambda(memberAccess, parameter);
        }

        public PropertyInfo GetKeyInfo()
        {
            var keyInfo = typeof (TEntity).GetProperties().FirstOrDefault(pi => pi.Name == KeyName);

            if (keyInfo == null) 
                throw new KeyNotFoundException("KeyName");

            return keyInfo;
        }

        public string EntityName 
        {
            get { return _entityMetadata.EntityName; }
        }

        public string EntityAlias
        {
            get { return _entityMetadata.EntityAlias; }
        }

        public string EntityDescription
        {
            get { return _entityMetadata.EntityDescription; }
        }

        #region GetTableName

        public string TableName
        {
            get { return Tools.GetTableName<TEntity>(_context); }
        }

        #endregion
    }
}