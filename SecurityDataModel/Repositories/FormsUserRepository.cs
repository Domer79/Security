using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class FormsUserRepository : UserRepositoryBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public FormsUserRepository(SecurityContext context) 
            : base(context)
        {
        }

        protected override void SetPasswordOrSid(User user, string passwordOrSid)
        {
            if (user == null) 
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(passwordOrSid))
                throw new ArgumentNullException("passwordOrSid");

            user.Password = SystemTools.Crypto.GetHashString(passwordOrSid);
            user.Usersid = null;
        }

        public override void Edit(string login, string email, string usersid, string displayName, string password)
        {
            if (GetUser(login, email, null, password) == null)
                throw new LoginEmailOrPasswordInvalidException();

            base.Edit(login, email, usersid, displayName, password);
        }

        public override void Delete(string login, string email, string usersid, string password)
        {
            if (GetUser(login, email, null, password) == null)
                throw new LoginEmailOrPasswordInvalidException();

            base.Delete(login, email, usersid, password);
        }

        protected override User GetUser(string login, string email, string usersid, string password)
        {
            var usersByLoginEmail = Repo.Where(u => u.Login == login || u.Email == email);
            var user = usersByLoginEmail.FirstOrDefault(u => u.Password == SystemTools.Crypto.GetHashString(password));

            return user;
        }

        public override IQueryable<IUser> GetQueryableCollection()
        {
            return Repo.Where(user => user.Password != null);
        }
    }
}
