using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using Interfaces;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Repository<User> _repo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public UserRepository(SecurityContext context)
        {
            _repo = new Repository<User>(context);
        }

        public void Add(string login, string displayName, string email, string usersid)
        {
            var user = CheckUser(login, email, usersid, () => new User {Login = login, DisplayName = displayName, Email = email, Usersid = usersid});

            if (_repo.Any(u => u.Login == login))
                throw new MemberExistsException();

            _repo.InsertOrUpdate(user);
            _repo.SaveChanges();
        }

        public void Edit(string login, string displayName, string email, string usersid)
        {
            var user = _repo.FirstOrDefault(u => u.Login == login || u.Usersid == usersid);

            if (user == null)
                throw new MemberNotFoundException(login, displayName, email, usersid);

            Edit(user.IdUser, login, displayName, email, usersid);
        }

        public void Edit(int idUser, string login, string displayName, string email, string usersid)
        {
            var usr = CheckUser(login, email, usersid, () =>
            {
                var findUser = _repo.Find(idUser);
                if (findUser == null)
                    throw new MemberNotFoundException(idUser, login, displayName, email, usersid);
                return findUser;
            });

            usr.DisplayName = displayName;
            usr.Email = email;

            _repo.SaveChanges();
        }

        public void Delete(string loginOrSid)
        {
            var user = _repo.FirstOrDefault(u => u.Login == loginOrSid || u.Usersid == loginOrSid);

            if (user == null)
                throw new MemberNotFoundException("Login: ", loginOrSid);

            Delete(user.IdUser);
        }

        public void Delete(int idUser)
        {
            var usr = _repo.Find(idUser);
            if (usr == null)
                throw new MemberNotFoundException(string.Format("Участник безопасности с таким Id = {0} не найден.", idUser));

            _repo.Delete(usr);
            _repo.SaveChanges();
        }

        private static User CheckUser(string login, string email, string usersid, Func<User> getUser)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("login");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("email");

            if (string.IsNullOrEmpty(usersid))
                throw new ArgumentException("usersid");

            var user = getUser();
            return user;
        }

        public IQueryable<IUser> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
