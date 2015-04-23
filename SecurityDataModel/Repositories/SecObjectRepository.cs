using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public abstract class SecObjectRepository<TSecObject> : ISecObjectRepository<TSecObject> 
        where TSecObject : SecObject, ISecObject
    {
        private readonly SecurityRepository<TSecObject> _repo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected SecObjectRepository()
        {
            _repo = new SecurityRepository<TSecObject>();
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

    /// <summary>
    /// Получает общий список всех обхектов безопасности
    /// </summary>
    public class SecObjectRepository : RepositoryBase<SecObject>
    {
        private static SecObjectRepository _instance;

        private SecObjectRepository()
        {
        }

        protected override DbContext GetContext()
        {
            return Tools.CreateContext();
        }

        private ISecObject GetSecObject(int idSecObject)
        {
            return Find(idSecObject);
        }

        private ISecObject GetSetObject(string objectName)
        {
            if (objectName == null || string.IsNullOrEmpty(objectName)) 
                throw new ArgumentNullException("objectName");

            return this.First(so => so.ObjectName == objectName);
        }

        public static ISecObject GetSecurityObject(int idSecObject)
        {
            return Instance.GetSecObject(idSecObject);
        }

        public static ISecObject GetSecurityObject(string objectName)
        {
            return Instance.GetSetObject(objectName);
        }

        private static SecObjectRepository Instance
        {
            get { return _instance ?? (_instance = new SecObjectRepository()); }
        }

        public static IQueryable<ISecObject> GetSecObjects()
        {
            return Instance.AsQueryable();
        }
    }
}
