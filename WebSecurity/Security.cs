using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using SystemTools.Exceptions;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
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
        private IPrincipal _principal;

        private Security()
        {
            SecurityDataModel.Service.SetContext(WebMvcSecurityContext.Create);
            WebSecurity.PublicRole.SetPublic();
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

        public string UserName
        {
            get { return Principal.Identity.Name; }
        }

        public IPrincipal Principal
        {
            get { return _principal ?? (_principal = new UserProvider(null)); }
            set { _principal = value; }
        }

        public void AddUser(string userName, string password, string email, string displayName, string sid)
        {
            var repo = new UserRepository();
            repo.Add(userName, password, displayName, email, sid);
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
            if (repo.GetSecObject(tableName) == null)
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

        public void SetPassword(string login, string password)
        {
            if (Tools.IsWindowsUser(login))
                return;

            var repo = new UserRepository();
            repo.SetPassword(login, password);
        }

        public void DeleteGrant(string roleName, string objectName, SecurityAccessType accessType)
        {
            var grantRepo = new GrantRepository();
            var role = RoleRepository.GetRoleObject(roleName);
            var secObject = SecObjectRepository.GetSecurityObject(objectName);
            var accessTypes = AccessTypeRepository.GetAccessTypes(accessType);

            if (secObject == null)
                throw new ArgumentException("objectName");

            if (role == null)
                throw new ArgumentException("roleName");

            if (accessTypes == null || accessTypes.Length == 0)
                throw new InvalidOperationException(string.Format("Тип доступа {0} отсутствует в базе данных", accessType));

            foreach (var access in accessTypes)
            {
                grantRepo.RemoveGrant(secObject, role, access);
            }
        }

        public void DeleteUser(string userName)
        {
            var repo = new UserRepository();
            repo.DeleteUser(userName);
        }

        public IEnumerable<IUser> GetUsers()
        {
            return UserRepository.GetUserCollection();
        }

        public IEnumerable<IGroup> GetGroups()
        {
            return GroupRepository.GetGroupCollection();
        }

        public IEnumerable<IRole> GetRoles()
        {
            return RoleRepository.GetRoleCollection();
        }

        public bool Sign(string login, string password)
        {
            var repo = new UserRepository();
            return repo.SignUser(login, password);
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
        /// <param name="skipError">Если True метод пропускает возникшие ошибки и продолжает работу</param>
        /// <exception cref="ArgumentException">Возникает в случае отсутствия значений какого-либо из входных параметров в базе данных</exception>
        public void Grant(string roleName, string objectName, SecurityAccessType accessType, bool skipError = false)
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
                try
                {
                    grantRepo.AddGrant(secObject, roleObject, access);
                }
                catch
                {
                    if (skipError)
                    {
                        grantRepo.DeleteFromContext(secObject, roleObject, access);
                        continue;
                    }

                    throw;
                }
            }
        }

        public void GrantToObjectCollection(string roleName, IEnumerable<string> objectNames,
            SecurityAccessType accessType)
        {
            if (objectNames == null) 
                throw new ArgumentNullException("objectNames");

            foreach (var objectName in objectNames)
            {
                try
                {
                    Grant(roleName, objectName, accessType, skipError: true);
                }
                catch
                {
                }
            }
        }

        public static void RenewContext()
        {
            SecurityDataModel.Service.RenewContext();
        }

        #endregion

        public static ISecurity Instance
        {
            get { return _instance ?? (_instance = new Security()); }
        }
    }
}