using DataRepository;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class SecurityRepository<TEntity> : Repository<TEntity> where TEntity : ModelBase
    {
        public SecurityRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
