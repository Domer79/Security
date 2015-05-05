using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using SystemTools;
using SystemTools.Extensions;
using DataRepository.Infrastructure;

namespace DataRepository
{
    public class RepositoryDataContext : DbContext
    {
        private ContextInfo _contextInfo;
        /// <summary>
        /// Создает новый экземпляр контекста с использованием соглашений для создания имени базы данных,     с которой будет установлено соединение.Имя по соглашению представляет собой полное имя (пространство имен + имя класса) производного класса контекста.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        protected RepositoryDataContext()
        {
            Init();
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием соглашений для создания имени базы данных, с которой будет установлено соединение, и инициализирует его из заданной модели.Имя по соглашению представляет собой полное имя (пространство имен + имя класса) производного класса контекста.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        /// <param name="model">Модель, которая будет поддерживать данный контекст.</param>
        protected RepositoryDataContext(DbCompiledModel model) 
            : base(model)
        {
            Init();
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием соглашений для создания имени или строки подключения базы данных, с которой будет установлено соединение.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        /// <param name="nameOrConnectionString">Имя базы данных или строка подключения.</param>
        public RepositoryDataContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
            Init();
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием указанной строки в качестве имени или строки подключения с базой данных, с которой будет установлено соединение, и инициализирует его из заданной модели.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        /// <param name="nameOrConnectionString">Имя базы данных или строка подключения.</param><param name="model">Модель, которая будет поддерживать данный контекст.</param>
        public RepositoryDataContext(string nameOrConnectionString, DbCompiledModel model) 
            : base(nameOrConnectionString, model)
        {
            Init();
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием существующего соединения с базой данных.Соединение не будет освобождено при освобождении контекста, если <paramref name="contextOwnsConnection"/> является false.
        /// </summary>
        /// <param name="existingConnection">Существующее соединение, которое будет использоваться новым контекстом.</param><param name="contextOwnsConnection">Если задано значение true, соединение освобождается при освобождении контекста. В противном случае за освобождение соединения отвечает вызывающая сторона.</param>
        public RepositoryDataContext(DbConnection existingConnection, bool contextOwnsConnection) 
            : base(existingConnection, contextOwnsConnection)
        {
            Init();
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием существующего соединения с базой данных и инициализирует его из заданной модели.Соединение не будет освобождено при освобождении контекста, если <paramref name="contextOwnsConnection"/> является false.
        /// </summary>
        /// <param name="existingConnection">Существующее соединение, которое будет использоваться новым контекстом.</param><param name="model">Модель, которая будет поддерживать данный контекст.</param><param name="contextOwnsConnection">Если задано значение true, соединение освобождается при освобождении контекста. В противном случае за освобождение соединения отвечает вызывающая сторона.</param>
        public RepositoryDataContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) 
            : base(existingConnection, model, contextOwnsConnection)
        {
            Init();
        }

        /// <summary>
        /// Создает новый экземпляр контекста на основе существующего объекта ObjectContext.
        /// </summary>
        /// <param name="objectContext">Существующий объект ObjectContext, который будет заключен в новый контекст.</param><param name="dbContextOwnsObjectContext">Если задано значение true, ObjectContext освобождается при освобождении DbContext. В противном случае за освобождение соединения отвечает вызывающая сторона.</param>
        public RepositoryDataContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) 
            : base(objectContext, dbContextOwnsObjectContext)
        {
            Init();
        }

        private void Init()
        {
            _contextInfo = ContextInfo.ContextInfoCollection[GetType()];
            _contextInfo.DatabaseName = Tools.GetDatabaseNameFromConnectionString(Database.Connection.ConnectionString);

            if (ApplicationCustomizer.EnableSecurity)
            {
                foreach(var entityType in ContextInfo.GetContextEntities(GetType()))
                {
                    _contextInfo.EntityMetadataCollection[entityType].TableName = GetTableName(entityType);
                }
            }
        }

        public string[] GetTableNames()
        {
            return ContextInfo.GetContextEntities(GetType()).Select(GetTableName).ToArray();
        }

        public string GetTableName(Type type)
        {
            if (type == null && !type.Is<ModelBase>())
                throw new ArgumentNullException("type");

            var sqlQuery = Set(type).AsNoTracking().ToString();
            return Tools.GetTableNameFromSqlQuery(sqlQuery);
        }

        #region EntityInfo members

        public string GetKeyName<TEntity>() where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]).KeyName;

            var entityInfo = new EntityInfo<TEntity>(this);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo.KeyName;
        }

        public Expression<Func<TEntity, TKey>> GetExpression<TEntity, TKey>() where TEntity : ModelBase
        {
            return GetExpression<TEntity, TKey>(null);
        }

        public Expression<Func<TEntity, TKey>> GetExpression<TEntity, TKey>(string columnName) where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]).GetMemberAccess<TKey>(columnName);

            var entityInfo = new EntityInfo<TEntity>(this);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo.GetMemberAccess<TKey>(columnName);
        }

        public Expression<Func<TEntity, object>> GetExpression<TEntity>(string columnName) where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]).GetMemberAccess(columnName);

            var entityInfo = new EntityInfo<TEntity>(this);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo.GetMemberAccess(columnName);
        }

        public EntityInfo<TEntity> GetEntityInfo<TEntity>() where TEntity : ModelBase
        {
            if (_entityInfos.Keys.Contains(typeof(TEntity)))
                return ((EntityInfo<TEntity>)_entityInfos[typeof(TEntity)]);

            var entityInfo = new EntityInfo<TEntity>(this);
            _entityInfos.Add(typeof(TEntity), entityInfo);

            return entityInfo;
        }

        private readonly Dictionary<Type, object> _entityInfos = new Dictionary<Type, object>();        
        
        #endregion
    }
}
