using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using SystemTools.Interfaces;
using WebSecurity.Data;
using WebSecurity.Infrastructure;

namespace WebSecurity.Repositories
{
    public class UserRepository : SecurityDataModel.Repositories.UserRepository
    {
        public IPrincipal GetUserPrincipal(string name)
        {
            return new UserProvider(GetUser(name));
        }

        public static IQueryable<IUser> GetUserCollection()
        {
            return new UserRepository().GetQueryableCollection();
        }
    }
}
