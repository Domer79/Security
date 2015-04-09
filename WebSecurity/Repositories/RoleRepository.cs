using SystemTools.Interfaces;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class RoleRepository : SecurityDataModel.Repositories.RoleRepository
    {
        public RoleRepository() 
            : base(new WebMvcSecurityContext())
        {
        }

        public static IRole GetRole(string role)
        {
            return new RoleRepository().GetRoleObject(role);
        }
    }
}
