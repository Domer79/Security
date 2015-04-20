using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using SecurityDataModel.Repositories;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense.Grant
{
    internal class SecObjectCommandTermList : IEnumerable<CommandTermBase>
    {
        private readonly IQueryable<ISecObject> _secObjects = SecObjectRepository.GetSecObjects();

        /// <summary>
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _secObjects.ToList().Select(so => new CommandTermSecObject(so.ObjectName)).GetEnumerator();
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