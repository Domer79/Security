using DataRepository;
using SecurityDataModel.Infrastructure;

namespace SecurityDataModel.Repositories
{
    public class SecurityRepository<TEntity> : Repository<TEntity> where TEntity : ModelBase
    {
        public SecurityRepository()
            : base(Tools.Context)
        {
        }
    }

}
