using System.Data.Entity;

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
