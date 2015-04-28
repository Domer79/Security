using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using SystemTools.Exceptions;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using SecurityDataModel.Models;
using SecurityDataModel.Repositories;
using WebSecurity.Data;
using WebSecurity.Infrastructure;
using WebSecurity.Repositories;
using AccessTypeRepository = WebSecurity.Repositories.AccessTypeRepository;
using GrantRepository = WebSecurity.Repositories.GrantRepository;
using GroupRepository = WebSecurity.Repositories.GroupRepository;
using RoleOfMemberRepository = WebSecurity.Repositories.RoleOfMemberRepository;
using RoleRepository = WebSecurity.Repositories.RoleRepository;
using UserGroupsDetailRepository = WebSecurity.Repositories.UserGroupsDetailRepository;
using UserRepository = WebSecurity.Repositories.UserRepository;

namespace WebSecurity
{
    public class Security : ISecurity
    {
        public const string AnonymousUser = "anonymous";
//        private readonly UserRepository _repo;
        private static Security _instance;

        private Security()
        {
            SecurityDataModel.Infrastructure.Tools.SetContext(WebMvcSecurityContext.Create);
            WebSecurity.PublicRole.SetPublic();
//            _repo = new UserRepository();
        }

        #region ISecurity members

        private IUser User { get; set; }

        public void SetAccessTypes<T>()
        {
            var accessRepo = new AccessTypeRepository();
            accessRepo.SetNewAccessTypes<T>();
        }

        public void SetAccessTypes<T1, T2>()
        {
            IAccessTypeRepository accessRepo = new AccessTypeRepository();
            accessRepo.SetNewAccessTypes<T1, T2>();
        }

        public void SetAccessTypes<T1, T2, T3>()
        {
            IAccessTypeRepository accessRepo = new AccessTypeRepository();
            accessRepo.SetNewAccessTypes<T1, T2, T3>();
        }

        public void SetAccessTypes<T1, T2, T3, T4>()
        {
            IAccessTypeRepository accessRepo = new AccessTypeRepository();
            accessRepo.SetNewAccessTypes<T1, T2, T3, T4>();
        }

        public IPublicRole PublicRole
        {
            get { return WebSecurity.PublicRole.Instance; }
        }

        public void AddUser(string userName, string password, string email, string displayName, string sid)
        {
            CreateUser(userName, password, email, displayName, sid);
        }

        public void AddGroup(string groupName, string description)
        {
            var repo = new GroupRepository();
            repo.Add(groupName, description);
        }

        public void AddRole(string roleName, string description)
        {
            var repo = new RoleRepository();
            repo.Add(roleName, description);
        }

        public void AddController(string path)
        {
            var repo = new ActionResultRepository();
            repo.Add(new ActionResultObject(){ObjectName = path});
        }

        public void AddTable(string tableName)
        {
            var repo = new TableObjectRepository();
            repo.Add(new TableObject(){ObjectName = tableName});
        }

        public void SetRole(string roleName, string memberName)
        {
            var repo = new RoleOfMemberRepository();
            repo.AddMemberToRole(memberName, roleName);
        }

        public void SetGroup(string groupName, string login)
        {
            var repo = new UserGroupsDetailRepository();
            repo.AddToGroup(login, groupName);
        }

        public void DeleteMemberFromRole(string memberName, string roleName)
        {
            var repo = new RoleOfMemberRepository();
            repo.DeleteMemberFromRole(memberName, roleName);
        }

        public void DeleteUserFromGroup(string userName, string groupName)
        {
            var repo = new UserGroupsDetailRepository();
            repo.DeleteFromGroup(userName, groupName);
        }

        public void DeleteGroup(string groupName)
        {
            var repo = new GroupRepository();
            repo.Delete(groupName);
        }

        public void DeleteController(string controllerName)
        {
            var repo = new ActionResultRepository();
            repo.DeleteSecObject(controllerName);
        }

        public void DeleteTable(string tableName)
        {
            var repo = new TableObjectRepository();
            repo.DeleteSecObject(tableName);
        }

        public bool Sign(string login, string password)
        {
            var repo = new UserRepository();
            return repo.SignUser(login, password);
        }

        internal void CreateUser(string login, string password, string displayName, string email, string sid, bool withPublic = true)
        {
            var repo = new UserRepository();
            repo.Add(login, password, displayName, email, sid);
            var newUser = repo.GetUser(login);
            if (withPublic)
            {
                var roleOfMembersRepo = new RoleOfMemberRepository();
                roleOfMembersRepo.AddMemberToRole(newUser, PublicRole);
            }
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
            var repo = new UserRepository();
            var cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return new UserProvider(User);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null)
                throw new LoginEmailOrPasswordInvalidException();

            return repo.GetUserPrincipal(ticket.Name);
        }

        public IPrincipal GetWindowsPrincipal(string name)
        {
            var repo = new UserRepository();
            if (Tools.IsWindowsUser(name))
                return repo.GetUserPrincipal(name);

            return new UserProvider(null);
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

        /// <summary>
        /// Предоставляет права для роли <see cref="roleName"/> для объекта <see cref="objectName"/>
        /// </summary>
        /// <param name="roleName">Имя роли</param>
        /// <param name="objectName">имя объекта</param>
        /// <param name="accessType">Тип доступа</param>
        /// <exception cref="ArgumentException">Возникает в случае отсутствия значений какого-либо из входных параметров в базе данных</exception>
        public void Grant(string roleName, string objectName, SecurityAccessType accessType)
        {
            var grantRepo = new GrantRepository();
            var roleObject = RoleRepository.GetRoleObject(roleName);
            var secObject = SecObjectRepository.GetSecurityObject(objectName);
            var accessTypes = AccessTypeRepository.GetAccessTypes(accessType);

            if (secObject == null)
                throw new ArgumentException("objectName");

            if (roleObject == null)
                throw new ArgumentException("roleName");

            if (accessTypes == null || accessTypes.Length == 0)
                throw new InvalidOperationException(string.Format("Тип доступа {0} отсутствует в базе данных", accessType));

            foreach (var access in accessTypes)
            {
                grantRepo.AddGrant(secObject, roleObject, access);
            }
        }

        public static void RenewContext()
        {
            SecurityDataModel.Infrastructure.Tools.RenewContext();
        }

        #endregion

        public static ISecurity Instance
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

            return query.Where(e => e.Name == Identity.Name && e.RoleName == role).Select(e => 1).Any();
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