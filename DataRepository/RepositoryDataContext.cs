using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using SystemTools;
using SystemTools.Exceptions;
using SystemTools.Extensions;
using SystemTools.WebTools.Infrastructure;
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

        /// <summary>
        /// Расширение, позволяющее пользователю настраивать проверку сущности или отфильтровать результаты проверки.Вызывается методом <see cref="M:System.Data.Entity.DbContext.GetValidationErrors"/>.
        /// </summary>
        /// <returns>
        /// Результат проверки сущности.Может содержать значение NULL при переопределении.
        /// </returns>
        /// <param name="entityEntry">Экземпляр DbEntityEntry, который должен быть проверен.
        /// </param><param name="items">Определяемый пользователем словарь, который содержит дополнительные сведения для пользовательской проверки.Он будет передан в объект     <see cref="T:System.ComponentModel.DataAnnotations.ValidationContext"/>     и предоставлен в качестве     свойства <see cref="P:System.ComponentModel.DataAnnotations.ValidationContext.Items"/>.Это необязательный параметр, он может содержать значение NULL.</param>
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);

            if (!ApplicationCustomizer.EnableSecurity)
                return result;

            if (ApplicationCustomizer.Security == null)
                throw new SecurityException2();

            SecurityAccessType securityAccessType;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    securityAccessType = SecurityAccessType.Insert;
                    break;
                case EntityState.Modified:
                    securityAccessType = SecurityAccessType.Update;
                    break;
                case EntityState.Deleted:
                    securityAccessType = SecurityAccessType.Delete;
                    break;
                default:
                    throw new SecurityException2(new InvalidOperationException("SecurityAccessType не инициализирован"));
            }

            var em = ContextInfo.ContextInfoCollection[GetType()].EntityMetadataCollection[entityEntry.Entity.GetType()];
            if (!ApplicationCustomizer.Security.IsAccess(em.EntityAlias, ApplicationCustomizer.Security.UserName, securityAccessType))
                result.ValidationErrors.Add(new DbValidationError("", string.Format("Отсутствуют права доступа на операцию {0} для объекта {1}", securityAccessType, em)));

            return result;
        }

        /// <summary>
        /// Расширение, позволяющее пользователю переопределить поведение по умолчанию, предполагающее проверку только добавленных и измененных сущностей.
        /// </summary>
        /// <returns>
        /// Значение true, если проверку следует продолжить. В противном случае — значение false.
        /// </returns>
        /// <param name="entityEntry">Проверяемый экземпляр DbEntityEntry.</param>
        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            if (!ApplicationCustomizer.EnableSecurity)
                return base.ShouldValidateEntity(entityEntry);

            if (entityEntry.State == EntityState.Deleted)
                return true;

            return base.ShouldValidateEntity(entityEntry);
        }
    }
}
