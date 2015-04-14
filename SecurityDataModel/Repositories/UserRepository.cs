using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Repository<User> _repo;

        public UserRepository()
        {
            _repo = new Repository<User>(Tools.Context);
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

        public IUser GetUser(string login)
        {
            var user = _repo.FirstOrDefault(u => u.Login == login);
            return user;
        }

        public bool SignUser(string login, string password)
        {
            var user = _repo.FirstOrDefault(u => u.Login == login); 
            return user != null && user.Password.SequenceEqual(password.GetHashBytes());
        }
    }
}