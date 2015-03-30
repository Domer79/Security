using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using SystemTools.Interfaces;
using WebSecurity.Data;
using WebSecurity.Infrastructure;

namespace WebSecurity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityDataModel.Repositories.UserRepository _repo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public UserRepository()
        {
            _repo = new SecurityDataModel.Repositories.UserRepository(new WebMvcSecurityContext());
        }

        public IQueryable<IUser> GetQueryableCollection()
        {
            return _repo.GetQueryableCollection();
        }

        public void Add(string login, string password)
        {
            string onlyLogin;
            string domain;
            if (Tools.IsWindowsUser(login, out onlyLogin, out domain))
                Add(onlyLogin, domain, password);
            else
                Add(login, Environment.MachineName, password);
        }

        public void Add(string login, string domain, string password)
        {
            _repo.Add(login, domain, password);
        }

        public void Add(string login, string domain, string password, string displayName, string email, string sid)
        {
            _repo.Add(login, domain, password, displayName, email, sid);
        }
    }
}
