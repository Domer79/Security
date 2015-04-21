using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class RoleRepository : SecurityDataModel.Repositories.RoleRepository
    {
        public static IRole GetRoleObject(string role)
        {
            return new RoleRepository().GetRole(role);
        }

        public static IRole GetRoleObject(int idRole)
        {
            return new RoleRepository().GetRole(idRole);
        }

        public static IQueryable<IRole> GetRoleCollection()
        {
            return new RoleRepository().GetQueryableCollection();
        }
    }
}
