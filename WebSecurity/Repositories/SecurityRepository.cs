using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Repositories;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class SecurityRepository<TEntity> : SecurityDataModel.Repositories.SecurityRepository<TEntity> 
        where TEntity : ModelBase
    {
    }
}
