using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermDispatcher<TCommandTermEntryPoint> : IEnumerable<string> where TCommandTermEntryPoint : CommandTermBase, new()
    {
//        private const string CommandStringPattern = @"(?<term>[^\W]+)(?<delimiter>[^\w]*)";
        private const string CommandStringPattern = @"[^\s]+";
        private const string EndSpacePattern = @"[\s]$";
        private readonly CommandTermBase _commandTermEntryPoint = new TCommandTermEntryPoint();
        private readonly CommandTermStack _stack = new CommandTermStack();

        public CommandTermDispatcher(string command)
        {
            CommandStrings = GetCommandStrings(command);
        }

        private string[] CommandStrings { get; set; }

        private CommandTermBase CommandTermEntryPoint
        {
            get { return _commandTermEntryPoint; }
        }

        private string[] GetCommandStrings(string command)
        {
            var rx = new Regex(CommandStringPattern);
//            var commandStrings = rx.Matches(command).OfType<Match>().Select(m => m.Groups["term"].Value).Union(GetEndSpace(command)).ToArray();
            var commandStrings = rx.Matches(command).OfType<Match>().Select(m => m.Value).Union(GetEndSpace(command)).ToArray();
            return commandStrings.Length == 0 ? new []{""} : commandStrings;
        }

        private IEnumerable<string> GetEndSpace(string command)
        {
            return Regex.IsMatch(command, EndSpacePattern) ? new[] {""} : new string[] {};
        }

        private IEnumerable<string> GetCommandTerms()
        {
            return GetCommandTerms(0, CommandTermEntryPoint);
        }

        private IEnumerable<string> GetCommandTerms(int termIndex, CommandTermBase commandTerm)
        {
            _stack.Add(commandTerm);
            var nextCommandTerms = commandTerm.GetNextCommandTerms();
            if (termIndex == CommandStrings.Length - 1)
//                return commandTerm.GetNextCommandTerms().Where(t => t.CommandTerm.StartsWith(CommandStrings[termIndex])).Select(t => t.ToString());
            {
                return nextCommandTerms == null ? new[]{""} : nextCommandTerms.Where(t => t.CommandTerm.ToLower().Contains(CommandStrings[termIndex].ToLower())).Select(t => t.ToString());
            }

            return GetCommandTerms(termIndex + 1, nextCommandTerms.First(t => t.CommandTerm == CommandStrings[termIndex]));
        }

        private IEnumerable<string> GetCommandTerms2(int termIndex, CommandTermBase commandTerm)
        {
            if (_stack.CanAdd(CommandStrings[termIndex]))
                _stack.Add(CommandStrings[termIndex]);
        }

        #region IEnumerable members

        /// <summary>
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            return GetCommandTerms().GetEnumerator();
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

        #endregion
    }

    public class CommandTermStack : ICollection<CommandTermBase>
    {
        private readonly List<CommandTermBase> _list = new List<CommandTermBase>();

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
        public void Add(CommandTermBase item)
        {
            _list.Add(item);
        }

        /// <summary>
        /// ���� CommandTerm � ��������� �������� ���������� CommandTerm � ��������� ��� � ����
        /// </summary>
        /// <param name="commandString">��� �������</param>
        public void Add(string commandString)
        {
            var commandTerm = GetLastCommandTerm();
        }

        private CommandTermBase GetLastCommandTerm()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ������� ��� �������� �� ��������� <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">������ <see cref="T:System.Collections.Generic.ICollection`1"/> �������� ������ ��� ������.</exception>
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    }
}