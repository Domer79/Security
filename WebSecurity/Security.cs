using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using SystemTools.Exceptions;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;
using WebSecurity.Data;
using WebSecurity.Repositories;
using AccessTypeRepository = WebSecurity.Repositories.AccessTypeRepository;
using GrantRepository = WebSecurity.Repositories.GrantRepository;
using RoleOfMemberRepository = WebSecurity.Repositories.RoleOfMemberRepository;
using RoleRepository = WebSecurity.Repositories.RoleRepository;
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
            Tools.SetContext(WebMvcSecurityContext.Create);
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

        public bool Sign(string login, string password)
        {
            var repo = new UserRepository();
            return repo.SignUser(login, password);
        }

        public void CreateUser(string login, string password, bool withPublic = true)
        {
            var repo = new UserRepository();
            repo.Add(login, password);
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
            return repo.GetUserPrincipal(name);
        }

        /// <summary>
        /// ���������� ���� �� ����� ������� � �������� ��������� � �������������� �������
        /// </summary>
        /// <param name="objectName">������, ������������� ����� �������</param>
        /// <param name="userName">��� ������������</param>
        /// <param name="accessType">��� �������</param>
        /// <returns></returns>
        public bool IsAccess(string objectName, string userName, SecurityAccessType accessType)
        {
            var repo = new GrantRepository();
            return accessType.GetFlags().All(@enum => repo.IsAccess(userName, objectName, @enum.ToString()));
        }

        /// <summary>
        /// ������������� ����� ���� <see cref="role"/> ��� ������� �������� ����������� 
        /// </summary>
        /// <param name="controller">��� �����������</param>
        /// <param name="action">��� �������</param>
        /// <param name="role">��� ����</param>
        /// <exception cref="ArgumentException">��������� � ������ ���������� �������� ������-���� �� ������� ���������� � ���� ������</exception>
        public void GrantActionToRole(string controller, string action, string role)
        {
            var grantRepo = new GrantRepository();
            var controllerObject = ActionResultRepository.GetActionResult(controller, action);
            var roleObject = RoleRepository.GetRoleObject(role);
            var accessTypeObject = AccessTypeRepository.GetExecAccessType();

            if (controllerObject == null)
                throw new ArgumentException("action");

            if (roleObject == null)
                throw new ArgumentException("role");

            if (accessTypeObject == null)
                throw new InvalidOperationException("��� ������� Exec ����������� � ���� ������");

            grantRepo.AddGrant(controllerObject, roleObject, accessTypeObject);
        }

        public void GrantEntityToRole(string entity, string role, SecurityAccessType accessType)
        {
            var grantRepo = new GrantRepository();
            var entityObject = TableObjectRepository.GetTableObject(entity);
            var roleObject = RoleRepository.GetRoleObject(role);
            var accessTypes = AccessTypeRepository.GetAccessTypes(accessType);

            if (entityObject == null)
                throw new ArgumentException("entity");

            if (roleObject == null)
                throw new ArgumentException("role");

            if (accessTypes == null || accessTypes.Length == 0)
                throw new ArgumentException("accessType");

            foreach (var access in accessTypes)
            {
                grantRepo.AddGrant(entityObject, roleObject, access);
            }
        }

        public static void RenewContext()
        {
            Tools.RenewContext();
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
        /// ����������, ��������� �� ������� �������� � ��������� ����.
        /// </summary>
        /// <returns>
        /// true, ���� ������� �������� �������� ��������� ��������� ����; � ��������� ������ � false.
        /// </returns>
        /// <param name="role">��� ����, ��� ������� ��������� ��������� ��������.</param>
        public bool IsInRole(string role)
        {
            var repo = new RoleOfMemberRepository();
            var query = repo.GetQueryableCollection();

            return query.Where(e => e.MemberName == Identity.Name && e.RoleName == role).Select(e => 1).Any();
        }

        /// <summary>
        /// ���������� ������������� �������� ���������.
        /// </summary>
        /// <returns>
        /// ������ <see cref="T:System.Security.Principal.IIdentity"/>, ��������� � ������� ����������.
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
        /// �������� ��� �������� ������������.
        /// </summary>
        /// <returns>
        /// ��� ������������, �� ���� �������� ����������� ��� ���������.
        /// </returns>
        public string Name
        {
            get { return IsAuthenticated ? _user.Login : Security.AnonymousUser; }
        }

        /// <summary>
        /// �������� ��� ������������ �������� �����������.
        /// </summary>
        /// <returns>
        /// ��� �������� �����������, ����������� ��� ������������� ������������.
        /// </returns>
        public string AuthenticationType
        {
            get { return typeof(User).ToString(); }
        }

        /// <summary>
        /// �������� ��������, ������������, ������ �� ������������ �������� �����������.
        /// </summary>
        /// <returns>
        /// true, ���� ������������ ������ �������� �����������; � ��������� ������ � false.
        /// </returns>
        public bool IsAuthenticated
        {
            get { return _user != null; }
        }

        /// <summary>
        /// ���������� ������, ������� ������������ ������� ������.
        /// </summary>
        /// <returns>
        /// ������, �������������� ������� ������.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}