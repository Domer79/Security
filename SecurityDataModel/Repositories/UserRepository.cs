using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Events.Delegates;
using SecurityDataModel.Events.EventArgs;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityRepository<User> _repo;
        private readonly object _lockObject = new object();

        public UserRepository()
        {
            _repo = new SecurityRepository<User>();
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

//            if (password == null) 
//                throw new ArgumentNullException("password");

            var user = new User
            {
                Login = login.ToLower(),
                Password = password == null ? null : password.GetHashBytes(),
                DisplayName = displayName,
                Email = email,
                Usersid = sid
            };

            _repo.InsertOrUpdate(user);
            _repo.SaveChanges();
            OnAddUser(user);
        }

        private void OnAddUser(User user)
        {
            var handler = UserAdded;
            if (handler != null)
            {
                handler(this, new UserAddedEventArgs(user));
            }
        }

        public event UserAddedEventHandler UserAdded;

        public IUser GetUser(string login)
        {
            lock (_lockObject)
            {
                var user = _repo.FirstOrDefault(u => u.Login == login);
                return user;
            }
        }

        public bool SignUser(string login, string password)
        {
            var user = _repo.FirstOrDefault(u => u.Login == login); 
            return user != null && user.Password.SequenceEqual(password.GetHashBytes());
        }

        public void SetPassword(string login, string password)
        {
            var user = _repo.First(u => u.Login == login);
            if (user == null)
                throw new ArgumentException("login");

            user.Password = password.GetHashBytes();

            _repo.SaveChanges();
        }

        public void DeleteUser(string login)
        {
            var user = _repo.First(u => u.Login == login);
            _repo.Delete(user);
            _repo.SaveChanges();
        }
    }
}