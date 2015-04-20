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
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _secObjects.ToList().Select(so => new CommandTermSecObject(so.ObjectName)).GetEnumerator();
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