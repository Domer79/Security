using System.Linq;

namespace DataRepository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        void InsertOrUpdate(T item);

        void Delete(T item);

        object GetDefaultKeyValue();
    }
}