using System.Linq;

namespace DataRepository.Interfaces
{
    public interface IRepository<T> : IQueryable<T> where T : class
    {
        IQueryable<T> AsQueryable();

        void InsertOrUpdate(T item);

        void SaveChanges();

        void Delete(T item);

        object GetDefaultKeyValue();
    }
}