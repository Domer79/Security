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

            return query.Where(e => e.Name == Identity.Name && e.RoleName == role).Select(e => 1).Any();
        }

        /// <summary>
        /// ���������� ������������� �������� ���������.
        /// </summary>
        /// <returns>
        /// ������ <see cref="T:System.Security.Principal.IIdentity"/>, ��������� � ������� ����������.
        /// </returns>
        public IIdentity Identity
        {
            get { return _identity; }
        }
    }
}