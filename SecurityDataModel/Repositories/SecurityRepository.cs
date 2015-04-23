using DataRepository;
using SecurityDataModel.Infrastructure;

namespace SecurityDataModel.Repositories
{
    public class SecurityRepository<TEntity> : SecurityRepositoryBase<TEntity> where TEntity : ModelBase
    {
    }
}
