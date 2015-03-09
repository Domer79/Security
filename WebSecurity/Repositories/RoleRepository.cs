using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class RoleRepository : SecurityDataModel.Repositories.RoleRepository
    {
        public RoleRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
