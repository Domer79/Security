using System;
using System.Security.Principal;
using SystemTools.Interfaces;
using SecurityDataModel.Models;

namespace WebSecurity
{
    public class UserIdentity : IIdentity
    {
        private readonly IUser _user;

        public UserIdentity(IUser user)
        {
            _user = user;
        }

        /// <summary>
        /// Получает имя текущего пользователя.
        /// </summary>
        /// <returns>
        /// Имя пользователя, от лица которого выполняется код программы.
        /// </returns>
        public string Name
        {
            get { return User.Login; }
        }

        /// <summary>
        /// Получает тип используемой проверки подлинности.
        /// </summary>
        /// <returns>
        /// Тип проверки подлинности, применяемой для идентификации пользователя.
        /// </returns>
        public string AuthenticationType
        {
            get { return typeof(User).ToString(); }
        }

        /// <summary>
        /// Получает значение, определяющее, прошел ли пользователь проверку подлинности.
        /// </summary>
        /// <returns>
        /// true, если пользователь прошел проверку подлинности; в противном случае — false.
        /// </returns>
        public bool IsAuthenticated
        {
            get { return User != AnonymousUser.Instance; }
        }

        public IUser User
        {
            get { return _user ?? AnonymousUser.Instance; }
        }

        /// <summary>
        /// Возвращает строку, которая представляет текущий объект.
        /// </summary>
        /// <returns>
        /// Строка, представляющая текущий объект.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}