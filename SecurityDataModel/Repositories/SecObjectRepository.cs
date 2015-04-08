using System.Linq;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public abstract class SecObjectRepository<TSecObject> : ISecObjectRepository<TSecObject> 
        where TSecObject : ModelBase, ISecObject
    {
        private readonly Repository<TSecObject> _repo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected SecObjectRepository(SecurityContext context)
        {
            _repo = new Repository<TSecObject>(context);
        }

        public IQueryable<TSecObject> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
