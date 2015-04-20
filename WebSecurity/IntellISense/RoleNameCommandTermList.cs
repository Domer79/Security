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
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _query.ToList().Select(r => new TCommandTermRoleName{RoleName = r.RoleName}).GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель, осуществляющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Collections.IEnumerator"/>, который может использоваться для перебора коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}