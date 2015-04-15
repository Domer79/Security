using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;

namespace DataRepository
{
    public class Repository<TEntity> : RepositoryBase<TEntity> 
        where TEntity : ModelBase
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override DbContext GetContext()
        {
            return _dbContext;
        }
    }
}
