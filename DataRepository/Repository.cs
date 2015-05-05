using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace DataRepository
{
    public class Repository<TEntity> : RepositoryBase<TEntity> 
        where TEntity : ModelBase
    {
        private readonly RepositoryDataContext _dbContext;

        public Repository(RepositoryDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override RepositoryDataContext GetContext()
        {
            return _dbContext;
        }
    }
}
