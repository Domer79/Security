using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using WebSecurity.IntellISense.Base;
using WebSecurity.IntellISense.Grant.AccessTypes;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense
{
    internal class RoleNameCommandTermList<TCommandTermRoleName> : IEnumerable<CommandTermBase> where TCommandTermRoleName : CommandTermRoleNameBase, new()
    {
        private readonly IQueryable<IRole> _query = new RoleRepository().GetQueryableCollection();

        /// <summary>
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _query.ToList().Select(r => new TCommandTermRoleName{RoleName = r.RoleName}).GetEnumerator();
        }

        /// <summary>
        /// ���������� �������������, �������������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ������ <see cref="T:System.Collections.IEnumerator"/>, ������� ����� �������������� ��� �������� ���������.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}