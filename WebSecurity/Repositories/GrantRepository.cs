using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class GrantRepository : SecurityDataModel.Repositories.GrantRepository
    {
        public GrantRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
