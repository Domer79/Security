using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class RoleRepository : SecurityDataModel.Repositories.RoleRepository
    {
        public RoleRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
