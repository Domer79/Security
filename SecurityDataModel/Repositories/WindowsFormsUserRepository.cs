using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class WindowsFormsUserRepository : UserRepositoryBase
    {
        public WindowsFormsUserRepository(SecurityContext context) 
            : base(context)
        {
        }

        protected override void SetPasswordOrSid(User user, string passwordOrSid)
        {
            if (!Tools.IsWindowsUser(user.Login, passwordOrSid))
                user.Password = passwordOrSid.GetBytes();

            user.Usersid = passwordOrSid;
        }

        protected override User GetUser(string login, string email, string usersid, string password)
        {
            if (Tools.IsWindowsUser(login, usersid))
                return Repo.Where(u => u.Usersid != null).FirstOrDefault(u => u.Login == login || u.Usersid == usersid);

            return FormsUserRepository.GetUser(Repo, login, email, password);
        }

        public override IQueryable<IUser> GetQueryableCollection()
        {
            return Repo;
        }
    }
}
