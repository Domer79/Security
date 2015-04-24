using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.Infrastructure;

namespace DataRepository
{
    public class RepositoryDataContext : DbContext
    {
        /// <summary>
        /// Создает новый экземпляр контекста с использованием соглашений для создания имени базы данных,     с которой будет установлено соединение.Имя по соглашению представляет собой полное имя (пространство имен + имя класса) производного класса контекста.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        protected RepositoryDataContext()
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием соглашений для создания имени базы данных, с которой будет установлено соединение, и инициализирует его из заданной модели.Имя по соглашению представляет собой полное имя (пространство имен + имя класса) производного класса контекста.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        /// <param name="model">Модель, которая будет поддерживать данный контекст.</param>
        protected RepositoryDataContext(DbCompiledModel model) 
            : base(model)
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием соглашений для создания имени или строки подключения базы данных, с которой будет установлено соединение.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        /// <param name="nameOrConnectionString">Имя базы данных или строка подключения.</param>
        public RepositoryDataContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием указанной строки в качестве имени или строки подключения с базой данных, с которой будет установлено соединение, и инициализирует его из заданной модели.Как это используется при создании соединения, см. в примечаниях к классу.
        /// </summary>
        /// <param name="nameOrConnectionString">Имя базы данных или строка подключения.</param><param name="model">Модель, которая будет поддерживать данный контекст.</param>
        public RepositoryDataContext(string nameOrConnectionString, DbCompiledModel model) 
            : base(nameOrConnectionString, model)
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием существующего соединения с базой данных.Соединение не будет освобождено при освобождении контекста, если <paramref name="contextOwnsConnection"/> является false.
        /// </summary>
        /// <param name="existingConnection">Существующее соединение, которое будет использоваться новым контекстом.</param><param name="contextOwnsConnection">Если задано значение true, соединение освобождается при освобождении контекста. В противном случае за освобождение соединения отвечает вызывающая сторона.</param>
        public RepositoryDataContext(DbConnection existingConnection, bool contextOwnsConnection) 
            : base(existingConnection, contextOwnsConnection)
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }

        /// <summary>
        /// Создает новый экземпляр контекста с использованием существующего соединения с базой данных и инициализирует его из заданной модели.Соединение не будет освобождено при освобождении контекста, если <paramref name="contextOwnsConnection"/> является false.
        /// </summary>
        /// <param name="existingConnection">Существующее соединение, которое будет использоваться новым контекстом.</param><param name="model">Модель, которая будет поддерживать данный контекст.</param><param name="contextOwnsConnection">Если задано значение true, соединение освобождается при освобождении контекста. В противном случае за освобождение соединения отвечает вызывающая сторона.</param>
        public RepositoryDataContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) 
            : base(existingConnection, model, contextOwnsConnection)
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }

        /// <summary>
        /// Создает новый экземпляр контекста на основе существующего объекта ObjectContext.
        /// </summary>
        /// <param name="objectContext">Существующий объект ObjectContext, который будет заключен в новый контекст.</param><param name="dbContextOwnsObjectContext">Если задано значение true, ObjectContext освобождается при освобождении DbContext. В противном случае за освобождение соединения отвечает вызывающая сторона.</param>
        public RepositoryDataContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) 
            : base(objectContext, dbContextOwnsObjectContext)
        {
            ContextInfo.ContextInfoCollection.Add(this);
        }
    }
}
