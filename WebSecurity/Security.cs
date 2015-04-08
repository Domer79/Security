using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using SystemTools.Exceptions;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using SecurityDataModel.Models;
using WebSecurity.Repositories;

namespace WebSecurity
{
    public class Security : ISecurity
    {
        public const string AnonymousUser = "anonymous";
        private const string DefaultPassword = "DefaultPassword";
        private readonly UserRepository _repo = new UserRepository();
        private static Security _instance;

        private Security()
        {
            var accessRepo = new AccessTypeRepository();
            accessRepo.SetNewAccessType<SecurityAccessType>();
        }

        #region ISecurity members

        private IUser User { get; set; }

        public bool Sign(string login, string password)
        {
            User = GetUser(login, password);
            return User != null;
        }

        public void CreateCookie(string login, bool isPersistent = false)
        {
            DateTime expiration = DateTime.Now.Add(FormsAuthentication.Timeout);
            var ticket = new FormsAuthenticationTicket(1, login, DateTime.Now, expiration, isPersistent,
                string.Empty, FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName){Value = encTicket, Expires = expiration};

            HttpContext.Current.Response.SetCookie(cookie);
        }

        public IPrincipal GetWebPrinicipal()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return new UserProvider(User);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null)
                throw new LoginEmailOrPasswordInvalidException();

            return _repo.GetUserPrincipal(ticket.Name);
        }

        public IPrincipal GetWindowsPrincipal(string name)
        {
            return _repo.GetUserPrincipal(name);
        }

        /// <summary>
        /// Определяет есть ли права доступа у текущего участника к запрашиваемому объекту
        /// </summary>
        /// <param name="objectName">Объект, запрашивающий права доступа</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="accessType">Тип доступа</param>
        /// <returns></returns>
        public bool IsAccess(string objectName, string userName, SecurityAccessType accessType)
        {
            var repo = new GrantRepository();
            return accessType.GetFlags().All(@enum => repo.IsAccess(userName, objectName, @enum.ToString()));
        }

        #endregion

        private IUser GetUser(string login, string password = DefaultPassword)
        {
            return _repo.GetUser(login, password);
        }

        public static Security Instance
        {
            get { return _instance ?? (_instance = new Security()); }
        }
    }

    public class UserProvider : IPrincipal
    {
        private readonly IUser _user;
        private IIdentity _identity;

        public UserProvider(IUser user)
        {
            _user = user;
        }

        /// <summary>
        /// Определяет, относится ли текущий участник к указанной роли.
        /// </summary>
        /// <returns>
        /// true, если текущий участник является элементом указанной роли; в противном случае — false.
        /// </returns>
        /// <param name="role">Имя роли, для которой требуется проверить членство.</param>
        public bool IsInRole(string role)
        {
            var repo = new RoleOfMemberRepository();
            var query = repo.GetQueryableCollection();

            return query.Where(e => e.MemberName == Identity.Name && e.RoleName == role).Select(e => 1).Any();
        }

        /// <summary>
        /// Возвращает удостоверение текущего участника.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Security.Principal.IIdentity"/>, связанный с текущим участником.
        /// </returns>
        public IIdentity Identity
        {
            get { return _identity ?? (_identity = new UserIdentity(_user)); }
        }
    }

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
            get { return IsAuthenticated ? _user.Login : Security.AnonymousUser; }
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
            get { return _user != null; }
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