using System;
using System.Linq;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public abstract class SecObjectRepository<TSecObject> : ISecObjectRepository<TSecObject> 
        where TSecObject : SecObject, ISecObject
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

        public void Add(TSecObject secObject)
        {
            if (secObject == null) 
                throw new ArgumentNullException("secObject");

            _repo.InsertOrUpdate(secObject);
            _repo.SaveChanges();
        }

        public TSecObject GetSecObject(string objectName)
        {
            return _repo.FirstOrDefault(so => so.ObjectName == objectName);
        }
    }
}
