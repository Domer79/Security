using System.Linq;

namespace Interfaces
{
    public interface IQueryableCollection<out T> where T : class
    {
        IQueryable<T> GetQueryableCollection();
    }
}