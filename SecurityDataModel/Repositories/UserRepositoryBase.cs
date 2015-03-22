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
    public abstract class UserRepositoryBase : IUserRepository
    {
        public const string EmptyLogin = "empty_login";

        private readonly Repository<User> _repo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected UserRepositoryBase(SecurityContext context)
        {
            _repo = new Repository<User>(context);
        }

        public void Add(string login, string email, string displayName, string passwordOrSid)
        {
            var user = CheckUser(login, email, passwordOrSid, () => CreateUser(login, email, displayName, passwordOrSid));

            if (_repo.Any(u => u.Login == login))
                throw new MemberExistsException();

            _repo.InsertOrUpdate(user);
            _repo.SaveChanges();
        }

        private User CreateUser(string login, string email, string displayName, string passwordOrSid)
        {
            var user = new User
            {
                Login = login, 
                DisplayName = displayName, 
                Email = email
            };

            SetPasswordOrSid(user, passwordOrSid);
            return user;
        }

        protected abstract void SetPasswordOrSid(User user, string passwordOrSid);

        public void Edit(string login, string email, string displayName, string passwordOrSid)
        {
            var user = _repo.FirstOrDefault(u => u.Login == login || u.Usersid == passwordOrSid);

            if (user == null)
                throw new MemberNotFoundException(login, displayName, email);

            Edit(user.IdUser, login, displayName, email, passwordOrSid);
        }

        public void Edit(int idUser, string login, string email, string displayName, string passwordOrSid)
        {
            var usr = CheckUser(login, email, passwordOrSid, () =>
            {
                var findUser = _repo.Find(idUser);
                if (findUser == null)
                    throw new MemberNotFoundException(idUser, login, email, displayName);
                return findUser;
            });

            usr.DisplayName = displayName;
            usr.Email = email;
            SetPasswordOrSid(usr, passwordOrSid);

            _repo.SaveChanges();
        }

        public void Delete(string loginOrSid)
        {
            var user = _repo.FirstOrDefault(u => u.Login == loginOrSid || u.Usersid == loginOrSid);

            if (user == null)
                throw new MemberNotFoundException("Login: ", loginOrSid);

            _repo.Delete(user);
            _repo.SaveChanges();
        }

        public void Delete(int idUser)
        {
            var usr = _repo.Find(idUser);
            if (usr == null)
                throw new MemberNotFoundException(string.Format("Участник безопасности с таким Id = {0} не найден.", idUser));

            _repo.Delete(usr);
            _repo.SaveChanges();
        }

        public void SetPassword(string loginOrEmail, string password)
        {
            var user = _repo.FirstOrDefault(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            if (user == null)
                throw new MemberNotFoundException(MemberType.User, loginOrEmail);

            user.Password = SystemTools.Crypto.GetHashString(password);
            _repo.SaveChanges();
        }

        private static User CheckUser(string login, string email, string passwordOrSid, Func<User> getUser)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("login");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("email");

            if (string.IsNullOrEmpty(passwordOrSid))
                throw new ArgumentException("passwordOrSid");

            var user = getUser();
            return user;
        }

        public IQueryable<IUser> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
