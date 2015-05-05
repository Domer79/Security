using System;
using DataRepository.Infrastructure;

namespace DataRepository.Exceptions
{
    public class EntityAccessDenied : Exception
    {
        public EntityAccessDenied(EntityMetadata entityMetadata)
            : base(string.Format("������ ��������. ������: {0}", entityMetadata))
        {
            
        }
    }
}