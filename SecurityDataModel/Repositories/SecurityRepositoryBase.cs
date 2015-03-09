using System;
using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public abstract class SecurityRepositoryBase<TEntity> : RepositoryBase<TEntity> 
        where TEntity : ModelBase
    {
        private readonly SecurityContext _securityContext;

        protected SecurityRepositoryBase(SecurityContext context)
        {
            if (context == null) 
                throw new ArgumentNullException("context");

            _securityContext = context;
        }

        protected sealed override DbContext GetContext()
        {
            return _securityContext;
        }
    }
}
