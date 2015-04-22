using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using DataRepository.Exceptions;
using DataRepository.Infrastructure;
using DataRepository.Interfaces;

namespace DataRepository
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : ModelBase
    {
        private DbContext _context;
        private DbSet<T> _set;
        private readonly Dictionary<Type, object> _entityInfos = new Dictionary<Type, object>();
        private readonly object _lockObject = new object();

        public virtual void SaveChanges()
        {
            var validationErrors = Context.GetValidationErrors();
            var changeTracker = Context.ChangeTracker.Entries();
            
            foreach (var validationError in validationErrors.SelectMany(entityValidationResult => entityValidationResult.ValidationErrors))
            {
                throw new RepositoryValidationException(validationError.ErrorMessage, validationError.PropertyName);
            }
            if (HasChanges)
            {
                lock (_lockObject)
                {
                    Context.SaveChanges();
                }
            }
        }

        public bool HasChanges
        {
            get { return Context.ChangeTracker.HasChanges(); }
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return this;
        }

        public T GetObjectByKey(object key)
        {
            return Set.Find(key);
        }

        public T Find(T editObject)
        {
            return Set.Find(GetKeyValue(editObject));
        }

        public T Find(params object[] keys)
        {
            if (keys == null) 
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            return Set.Find(keys);
        }

        public T Create()
        {
            return Set.Create();
        }

        public virtual IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return Set.Include(path);
        }

        public virtual void InsertOrUpdate(T item)
        {
            if (!item.TheKey.Equals(GetDefaultKeyValue()))
            {
                Context.Entry(item).State = EntityState.Modified;
            }
            else
            {
                Set.Add(item);
            }
        }

        public virtual void Delete(T item)
        {
            Set.Remove(item);
        }

        public virtual object GetDefaultKeyValue()
        {
            return default(int);
        }

        protected DbSet<T> Set
        {
            get { return _set ?? (_set = Context.Set<T>()); }
        }

        private object GetKeyValue(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var pi = typeof(T).GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute))) ?? GetEntityInfo<T>().GetKeyInfo();

            return pi.GetValue(item, null);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return ((IQueryable<T>)Set).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region IQueryable<T>

        public virtual Expression Expression
        {
            get { return ((IQueryable<T>) Set).Expression; }
        }

        public Type ElementType
        {
            get { return ((IQueryable<T>)Set).ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return ((IQueryable) Set).Provider; }
        }

        public DbContext Context
        {
            get { return _context ?? (_context = GetContext()); }
        }

        protected abstract DbContext GetContext();

        #endregion

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter) Context).ObjectContext;
        }

        public string GetKeyName()
        {
            return GetKeyName<T>();
        }

        public string GetKeyName<TEntity>() where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]).KeyName;

            var entityInfo = new EntityInfo<TEntity>(Context);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo.KeyName;
        }

        public string GetTableName<TEntity>() where TEntity : ModelBase
        {
            var entityInfo = new EntityInfo<TEntity>(Context);
            return entityInfo.TableName;
        }

        public Expression<Func<TEntity, TKey>> GetExpression<TEntity, TKey>() where TEntity : ModelBase
        {
            return GetExpression<TEntity, TKey>(null);
        }

        public Expression<Func<TEntity, TKey>> GetExpression<TEntity, TKey>(string columnName) where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]).GetMemberAccess<TKey>(columnName);

            var entityInfo = new EntityInfo<TEntity>(Context);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo.GetMemberAccess<TKey>(columnName);
        }

        public Expression<Func<TEntity, object>> GetExpression<TEntity>(string columnName) where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]).GetMemberAccess(columnName);

            var entityInfo = new EntityInfo<TEntity>(Context);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo.GetMemberAccess(columnName);
        }

        public EntityInfo<TEntity> GetEntityInfo<TEntity>() where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]);

            var entityInfo = new EntityInfo<TEntity>(Context);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo;
        }

        public void ExecuteNonQuery(string query, params object[] parameters)
        {
            Context.Database.ExecuteSqlCommand(query, parameters);
        }

        public DbRawSqlQuery<TElement> SqlQuery<TElement>(string query, params object[] parameters)
        {
            return Context.Database.SqlQuery<TElement>(query, parameters);
        }
    }
}