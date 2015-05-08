using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using SystemTools.Interfaces;
using SecurityDataModel.Events.EventArgs;
using WebSecurity.Data;
using WebSecurity.Infrastructure;

namespace WebSecurity.Repositories
{
    public class UserRepository : SecurityDataModel.Repositories.UserRepository
    {
        public UserRepository()
        {
            UserAdded += UserRepository_UserAdded;
        }

        void UserRepository_UserAdded(object sender, UserAddedEventArgs args)
        {
            var user = args.User;
            var roleOfMember = new RoleOfMemberRepository();
            roleOfMember.AddMemberToRole(user, PublicRole.Instance);
        }

        public IPrincipal GetUserPrincipal(string name)
        {
            var user = GetUser(name);
            return new UserProvider(user);
        }

        public static IQueryable<IUser> GetUserCollection()
        {
            return new UserRepository().GetQueryableCollection();
        }
    }
}
