using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class UserRepository : IUserRepository
    {
        public const string EmptyLogin = "empty_login";

        private readonly Repository<User> _repo;

        public UserRepository(SecurityContext context)
        {
            _repo = new Repository<User>(context);
        }


        public IQueryable<IUser> GetQueryableCollection()
        {
            return _repo;
        }

        public void Add(string login, string domain, string password)
        {
            Add(login, domain, password, null, null, null);
        }

        public void Add(string login, string domain, string password, string displayName, string email, string sid)
        {
            if (login == null) 
                throw new ArgumentNullException("login");

            if (domain == null) 
                throw new ArgumentNullException("domain");

            if (password == null) 
                throw new ArgumentNullException("password");

            var user = new User
            {
                Login = login,
                Domain = domain,
                Password = password.GetHashBytes(),
                DisplayName = displayName,
                Email = email,
                Usersid = sid
            };

            _repo.InsertOrUpdate(user);
            _repo.SaveChanges();
        }
    }
}