using DataRepository;
using SecurityDataModel.Infrastructure;

namespace SecurityDataModel.Repositories
{
    public class SecurityRepository<TEntity> : SecurityRepositoryBase<TEntity> where TEntity : ModelBase
    {
        public new void SetContext(RepositoryDataContext context)
        {
            base.SetContext(context);
        }
    }
}