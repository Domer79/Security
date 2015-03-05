using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TaxorgRepository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        void InsertOrUpdate(T item);

        void Delete(T item);

        object GetDefaultKeyValue();
    }
}