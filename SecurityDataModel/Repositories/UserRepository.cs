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

        public void Edit(IUser user)
        {
            var usr = CheckUser(user.Login, user.Email, user.Usersid, () =>
            {
                var findUser = _repo.Find(user.IdUser);
                if (findUser == null)
                    throw new MemberNotFoundException(user);
                return findUser;
            });

            usr.DisplayName = user.DisplayName;
            usr.Email = user.Email;

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
