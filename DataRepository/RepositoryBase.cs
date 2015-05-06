using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using SystemTools;
using SystemTools.WebTools.Infrastructure;
using DataRepository.Exceptions;
using DataRepository.Infrastructure;
using DataRepository.Interfaces;

namespace DataRepository
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : ModelBase
    {
        private RepositoryDataContext _context;
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

        public void DeleteFromContext(T item)
        {
            Set.Remove(item);
        }

        public EntityState this[T item]
        {
            get { return Context.Entry(item).State; }
            set { Context.Entry(item).State = value; }
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
//                CheckAccess(SecurityAccessType.Update);
                Context.Entry(item).State = EntityState.Modified;
            }
            else
            {
//                CheckAccess(SecurityAccessType.Insert);
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

            var pi = typeof(T).GetProperties().FirstOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute))) ?? EntityInfo.GetKeyInfo();

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
            get
            {
                return ((IQueryable<T>) Set).Expression;
            }
        }

        public Type ElementType
        {
            get
            {
                return ((IQueryable<T>)Set).ElementType;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return ((IQueryable) Set).Provider;
            }
        }

        public static bool RepositoryForWeb
        {
            get { return ApplicationCustomizer.ApplicationType == ApplicationType.Web; }
        }

        public RepositoryDataContext Context
        {
            get
            {
                return _context ?? (_context = GetContext());
            }
        }

        protected abstract RepositoryDataContext GetContext();

        #endregion

        public ObjectContext GetObjectContext()
        {
            return ((IObjectContextAdapter) Context).ObjectContext;
        }

        public EntityInfo<T> EntityInfo
        {
            get { return GetEntityInfo(); }
        }

        public string GetKeyName()
        {
            return EntityInfo.KeyName;
        }

        public string GetTableName()
        {
            return EntityInfo.TableName;
        }

        public Expression<Func<T, TKey>> GetExpression<TKey>()
        {
            return GetExpression<TKey>(null);
        }

        public Expression<Func<T, TKey>> GetExpression<TKey>(string columnName)
        {
            return EntityInfo.GetMemberAccess<TKey>(columnName);
        }

        public Expression<Func<T, object>> GetExpression(string columnName)
        {
            return EntityInfo.GetMemberAccess(columnName);
        }

        public EntityInfo<T> GetEntityInfo()
        {
            if (_entityInfos.Keys.Contains(typeof(T)))
                return ((EntityInfo<T>)_entityInfos[typeof(T)]);

            var entityInfo = new EntityInfo<T>(Context);
            _entityInfos.Add(typeof(T), entityInfo);

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