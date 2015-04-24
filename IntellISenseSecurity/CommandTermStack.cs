using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Extensions;
using IntellISenseSecurity.Base;

namespace IntellISenseSecurity
{
    public class CommandTermStack: ICollection<CommandTermBase>
    {
        private readonly List<CommandTermBase> _list = new List<CommandTermBase>();
        private readonly ICommandTermTrigger[] _triggers;

        public CommandTermStack(CommandTermEntryPoint entryPoint)
        {
            if (entryPoint == null) 
                throw new ArgumentNullException("entryPoint");

            _list.Add(entryPoint);
            _triggers = entryPoint.Triggers;
        }

        /// <summary>
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _list.GetEnumerator();
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

        /// <summary>
        /// ��������� ������� � ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">������, ����������� � ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">������ <see cref="T:System.Collections.Generic.ICollection`1"/> �������� ������ ��� ������.</exception>
        [Obsolete("�� ��������������. ����������� Add(string commandString)", true)]
        void ICollection<CommandTermBase>.Add(CommandTermBase item)
        {
            throw new NotSupportedException("�� ��������������. ����������� Add(string commandString)");
        }

        /// <summary>
        /// ���� CommandTerm � ��������� �������� ���������� CommandTerm � ��������� ��� � ����
        /// </summary>
        /// <param name="commandString">��� �������</param>
        public void Add(string commandString)
        {
            if (string.IsNullOrEmpty(commandString)) 
                throw new ArgumentNullException("commandString");

            if (!CanAdd(commandString))
                throw new InvalidOperationException(string.Format("������ ����� {0} �����������", commandString));

            _list.Add(LastCommandTerm.First(ct => ct.CommandTerm == commandString));
            DoTrigger();
        }

        private void DoTrigger()
        {
            var commandTermTypes = _list.Select(ct => ct.GetType()).Where(t => !t.Is<CommandTermEntryPoint>()).ToArray();
            //TODO: ������������� � ��������� LINQ, ����� ����������� ���� ������
            foreach (var commandTermTrigger in _triggers)
            {
                foreach (var types in commandTermTrigger.CommandTermTypes)
                {
                    if (types.SequenceEqual(commandTermTypes, new CommandTermTypeComparer()))
                        commandTermTrigger.Trigger(LastCommandTerm);
                }
            }
        }

        /// <summary>
        /// ������� ��� �������� �� ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">������ <see cref="T:System.Collections.Generic.ICollection`1"/> �������� ������ ��� ������.</exception>
        [Obsolete("�� ��������������", true)]
        void ICollection<CommandTermBase>.Clear()
        {
            throw new NotSupportedException("�� ��������������");
        }

        /// <summary>
        /// ����������, �������� �� ��������� <see cref="T:System.Collections.Generic.ICollection`1"/> ��������� ��������.
        /// </summary>
        /// <returns>
        /// �������� true, ���� �������� <paramref name="item"/> ������ � ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>; � ��������� ������ � �������� false.
        /// </returns>
        /// <param name="item">������, ������� ��������� ����� � <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        [Obsolete("�� ��������������", true)]
        bool ICollection<CommandTermBase>.Contains(CommandTermBase item)
        {
            throw new NotSupportedException("�� ��������������");
        }

        /// <summary>
        /// �������� �������� <see cref="T:System.Collections.Generic.ICollection`1"/> � ������ <see cref="T:System.Array"/>, ������� � ���������� ������� <see cref="T:System.Array"/>.
        /// </summary>
        /// <param name="array">���������� ������ <see cref="T:System.Array"/>, � ������� ���������� �������� �� ���������� <see cref="T:System.Collections.Generic.ICollection`1"/>. ������ <see cref="T:System.Array"/> ������ ����� ����������, ������������ � ����.</param><param name="arrayIndex">������������� �� ���� ������ � ������� <paramref name="array"/>, ����������� ������ �����������.</param><exception cref="T:System.ArgumentNullException">�������� <paramref name="array"/> ����� �������� null.</exception><exception cref="T:System.ArgumentOutOfRangeException">�������� ��������� <paramref name="arrayIndex"/> ������ 0.</exception><exception cref="T:System.ArgumentException">���������� ��������� � �������� ��������� <see cref="T:System.Collections.Generic.ICollection`1"/> ��������� ��������� �����, ������� � ������� <paramref name="arrayIndex"/> �� ����� ������� ���������� <paramref name="array"/>.</exception>
        public void CopyTo(CommandTermBase[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// ������� ������ ��������� ���������� ������� �� ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// �������� true, ���� ������� <paramref name="item"/> ������� ������ �� <see cref="T:System.Collections.Generic.ICollection`1"/>, � ��������� ������ � �������� false. ���� ����� ����� ���������� �������� false, ���� �������� <paramref name="item"/> �� ������ � �������� ���������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">������, ������� ���������� ������� �� ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">������ <see cref="T:System.Collections.Generic.ICollection`1"/> �������� ������ ��� ������.</exception>
        [Obsolete("�� ��������������", true)]
        bool ICollection<CommandTermBase>.Remove(CommandTermBase item)
        {
            throw new NotSupportedException("�� ��������������");
        }

        /// <summary>
        /// �������� ����� ���������, ������������ � ���������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// ����� ���������, ������������ � ���������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// �������� ��������, �����������, �������� �� <see cref="T:System.Collections.Generic.ICollection`1"/> ������ ��� ������.
        /// </summary>
        /// <returns>
        /// �������� true, ���� <see cref="T:System.Collections.Generic.ICollection`1"/> �������� ������ ��� ������; � ��������� ������ � �������� false.
        /// </returns>
        bool ICollection<CommandTermBase>.IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// ������ ���� ���-������� ��� ������������� ����.
        /// </summary>
        /// <returns>
        /// ���-��� ��� �������� ������� <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = _list.Aggregate(0, (current, commandTerm) => current ^ commandTerm.GetType().GetHashCode());
            return hashCode;
        }

        public bool CanAdd(string commandString)
        {
            return LastCommandTerm.Any(ct => ct.CommandTerm == commandString);
        }

        internal CommandTermBase LastCommandTerm
        {
            get { return _list.Last(); }
        }

        public IEnumerable<CommandTermBase> LastCommandNext()
        {
            return LastCommandTerm;
        }

        public CommandTermBase this[int index]
        {
            get { return _list[index]; }
        }

        public CommandTermBase this[string term]
        {
            get { return this.First(ct => ct.CommandTerm == term); }
        }

        public T GetCommandTerm<T>() where T : CommandTermBase
        {
            try
            {
                return GetCommandTerm<T>(0);
            }
            catch
            {
                return null;
            }
        }

        public T GetCommandTerm<T>(int index) where T : CommandTermBase
        {
            return this.OfType<T>().ElementAt(index);
//            return (T)this.First(ct => ct.GetType() == typeof(T));
        }

        public void AddAdditionCommandTerm(string term)
        {
            _list.Add(new CommandTermAdditionalParam(term));
        }

        public CommandTermAdditionalParam[] GetAdditionalParams()
        {
            return this.OfType<CommandTermAdditionalParam>().ToArray();
        }

        public IEnumerable<CommandTermBase> GetLastCommandsNextNotAdditional()
        {
            return _list.Last(ct => !ct.Is<CommandTermAdditionalParam>());
        }
    }
}