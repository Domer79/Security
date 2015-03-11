using System;

namespace Interfaces
{
    public interface IAccessTypeRepository : IQueryableCollection<IAccessType>
    {
        void SetNewAccessType<T>();
        void SetNewAccessTypes(Type type);
        void SetNewAccessTypes(string[] accessNames);
    }
}