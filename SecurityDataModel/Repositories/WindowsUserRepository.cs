using System;
using System.Linq;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class WindowsUserRepository : UserRepositoryBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public WindowsUserRepository(SecurityContext context) 
            : base(context)
        {
        }

        protected override User GetUser(string login, string email, string usersid, string password)
        {
            return Repo
                    .Where(user => user.Usersid != null)
                    .FirstOrDefault(user => user.Login == login || user.Usersid == usersid);
        }

        protected override void SetPasswordOrSid(User user, string passwordOrSid)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            if (TypeOperation != OperationType.Add)
                return;

            if (!Tools.IsWindowsUser(user.Login, passwordOrSid))
                throw new ArgumentException("passwordOrSid");

            user.Usersid = passwordOrSid;
            user.Password = null;
        }

        public override IQueryable<IUser> GetQueryableCollection()
        {
            return Repo.Where(user => user.Usersid != null);
        }
    }
}
