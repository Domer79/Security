using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Extensions;

namespace DataRepository.Infrastructure
{
    public class EntityMetadataCollection : IEnumerable<EntityMetadata>
    {
        private readonly Dictionary<Type, EntityMetadata> _metadataCollection = new Dictionary<Type, EntityMetadata>();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        internal EntityMetadataCollection()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        internal EntityMetadataCollection(Type contextType)
        {
            if (!contextType.Is<RepositoryDataContext>())
                throw new ArgumentException("contextType");

            foreach (var entityType in ContextInfo.GetContextEntities(contextType))
            {
                Add(entityType);
            }
        }

        internal void Add(Type entityType)
        {
            _metadataCollection.Add(entityType, new EntityMetadata(entityType));
        }

        public EntityMetadata this[Type entityType]
        {
            get { return _metadataCollection[entityType]; }
        }

        public EntityMetadata this[string tableName]
        {
            get { return _metadataCollection.Values.FirstOrDefault(em => em.TableName == tableName); }
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<EntityMetadata> GetEnumerator()
        {
            return _metadataCollection.Values.GetEnumerator();
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
