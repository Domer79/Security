using System;
using System.Linq;
using DataRepository.Infrastructure;

namespace DataRepository.Exceptions
{
    public class EntityAccessDeniedException : Exception
    {
        public EntityAccessDeniedException(EntityMetadata entityMetadata)
            : this(new []{entityMetadata})
        {
            
        }

        public EntityAccessDeniedException(params EntityMetadata[]entityMetadatas)
            : base(string.Format("Доступ запрещен. Объект: {0}", entityMetadatas.Select(em => em.ToString()).Aggregate((current, next) => string.Format("{0}; {1}", current, next))))
        {
        }
    }
}