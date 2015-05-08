using System.Linq;
using System.Security.Principal;
using SystemTools.Interfaces;
using WebSecurity.Repositories;

namespace WebSecurity
{
    public class UserProvider : IPrincipal
    {
        private readonly IIdentity _identity;

        public UserProvider(IUser user)
        {
            _identity = new UserIdentity(user);
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
            get { return _identity; }
        }
    }
}