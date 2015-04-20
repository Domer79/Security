using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SystemTools.Extensions;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermDispatcher<TCommandTermEntryPoint> : IEnumerable<string> where TCommandTermEntryPoint : CommandTermEntryPoint, new()
    {
//        private const string CommandStringPattern = @"(?<term>[^\W]+)(?<delimiter>[^\w]*)";
        private const string CommandStringPattern = @"[^\s]+";
        private const string EndSpacePattern = @"[\s]$";
        private readonly CommandTermStack _stack;

        public CommandTermDispatcher(string command)
        {
            CommandStrings = GetCommandStrings(command);
            _stack = new CommandTermStack(new TCommandTermEntryPoint());
        }

        private string[] CommandStrings { get; set; }

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
//            return GetCommandTerms(0, new TCommandTermEntryPoint());
            return GetCommandTerms2(0);
        }

        private IEnumerable<string> GetCommandTerms(int termIndex, IEnumerable<CommandTermBase> commandTerm)
        {
            if (termIndex == CommandStrings.Length - 1)
//                return commandTerm.GetNextCommandTerms().Where(t => t.CommandTerm.StartsWith(CommandStrings[termIndex])).Select(t => t.ToString());
            {
                return commandTerm == null ? new[]{""} : commandTerm.Where(t => t.CommandTerm.ToLower().Contains(CommandStrings[termIndex].ToLower())).Select(t => t.ToString());
            }

            return GetCommandTerms(termIndex + 1, commandTerm.First(t => t.CommandTerm == CommandStrings[termIndex]));
        }

        private IEnumerable<string> GetCommandTerms2(int termIndex)
        {
            if (termIndex > CommandStrings.Length - 1)
                return new[] {""};

            var term = CommandStrings[termIndex];
            if (!_stack.CanAdd(term))
            {
                return _stack.LastCommandNext().Where(t => t.CommandTerm.ToLower().Contains(term.ToLower())).Select(t => t.ToString());
            }

            _stack.Add(term);
            return GetCommandTerms2(termIndex + 1);
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

    public interface ICommandTermTrigger
    {
        Type[] CommandTermTypes { get; }
        Action<CommandTermBase> Trigger { get; }
    }

    internal class CommandTermStack: ICollection<CommandTermBase>
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
            foreach (var commandTermTrigger in _triggers)
            {
                if (commandTermTrigger.CommandTermTypes.SequenceEqual(commandTermTypes))
                    commandTermTrigger.Trigger(LastCommandTerm);
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
    }
}