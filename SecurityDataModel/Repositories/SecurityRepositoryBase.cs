using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public abstract class SecurityRepositoryBase<TEntity> : RepositoryBase<TEntity> 
        where TEntity : ModelBase
    {
        private readonly SecurityContext _securityContext;

        protected SecurityRepositoryBase()
        {
            _securityContext = Tools.Context;
        }

        protected sealed override DbContext GetContext()
        {
            return _securityContext;
        }
    }
}
