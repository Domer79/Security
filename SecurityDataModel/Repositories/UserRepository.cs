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
        private readonly Repository<User> _repo;

        public UserRepository(SecurityContext context)
        {
            _repo = new Repository<User>(context);
        }


        public IQueryable<IUser> GetQueryableCollection()
        {
            return _repo;
        }

        public void Add(string login, string password)
        {
            Add(login, password, null, null, null);
        }

        public void Add(string login, string password, string displayName, string email, string sid)
        {
            if (login == null) 
                throw new ArgumentNullException("login");

            if (password == null) 
                throw new ArgumentNullException("password");

            var user = new User
            {
                Login = login,
                Password = password.GetHashBytes(),
                DisplayName = displayName,
                Email = email,
                Usersid = sid
            };

            _repo.InsertOrUpdate(user);
            _repo.SaveChanges();
        }

        public IUser GetUser(string login, string password)
        {
            var user = _repo.FirstOrDefault(u => u.Login == login && u.Password.SequenceEqual(password.GetHashBytes()));
            return user;
        }
    }
}