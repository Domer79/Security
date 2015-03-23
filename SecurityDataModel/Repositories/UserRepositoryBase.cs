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
    internal abstract class UserRepositoryBase
    {
        public const string EmptyLogin = "empty_login";
        protected internal OperationType _operationType;

        private readonly Repository<User> _repo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected UserRepositoryBase(SecurityContext context)
        {
            _repo = new Repository<User>(context);
        }

        public Repository<User> Repo
        {
            get { return _repo; }
        }

        public void Add(string login, string email, string displayName, string passwordOrSid)
        {
            _operationType = OperationType.Add;
            var user = CheckUser(login, email, passwordOrSid, () => CreateUser(login, email, displayName, passwordOrSid));

            if (Repo.Any(u => u.Login == login))
                throw new MemberExistsException();

            Repo.InsertOrUpdate(user);
            Repo.SaveChanges();
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

        public virtual void Edit(string login, string email, string usersid, string displayName, string password)
        {
            _operationType = OperationType.Edit;
            var user = GetUser(login, email, usersid, password);

            if (user == null)
                throw new MemberNotFoundException(login, displayName, email, "Проверьте правильность введенного пароля");

            user.DisplayName = displayName;
            user.Email = email;
            SetPasswordOrSid(user, password);

            Repo.SaveChanges();
        }

        /// <summary>
        /// Производит удаление пользователя по его логину, либо email-у, либо SID (для Windows, в этом случае пароль не обязателен)
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        public virtual void Delete(string login, string password)
        {
            Delete(login, null, password);
        }

        /// <summary>
        /// Производит удаление пользователя по его логину, либо email-у, либо SID (для Windows, в этом случае пароль не обязателен)
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="email">Почтовый адрес</param>
        /// <param name="password">Пароль</param>
        public virtual void Delete(string login, string email, string password)
        {
            Delete(login, email, null, password);
        }

        /// <summary>
        /// Производит удаление пользователя по его логину, либо email-у, либо SID (для Windows, в этом случае пароль не обязателен)
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="email">Почтовый адрес</param>
        /// <param name="usersid">SID для Windows пользователей</param>
        /// <param name="password">Пароль</param>
        public virtual void Delete(string login, string email, string usersid, string password)
        {
            _operationType = OperationType.Delete;
            var user = GetUser(login, email, usersid, password);

            if (user == null)
                throw new MemberNotFoundException(login, email, usersid, "Проверьте правильность введенного пароля");

            Repo.Delete(user);
            Repo.SaveChanges();
        }

        protected abstract User GetUser(string login, string email, string usersid, string password);

        public void SetPassword(string loginOrEmail, string password)
        {
            var user = Repo.FirstOrDefault(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            if (user == null)
                throw new MemberNotFoundException(MemberType.User, loginOrEmail);

            user.Password = SystemTools.Crypto.GetHashString(password);
            Repo.SaveChanges();
        }

        private static User CheckUser(string login, string email, string password, Func<User> getUser)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("login");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("email");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("password");

            var user = getUser();
            return user;
        }

        public abstract IQueryable<IUser> GetQueryableCollection();

        internal protected enum OperationType
        {
            Add,
            Edit,
            Delete
        }
    }
}
