using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntellISenseSecurity.Base
{
    public abstract class CommandTermBase : IEnumerable<CommandTermBase>
    {
        private string _commandTerm;

        public string CommandTerm
        {
            get { return _commandTerm ?? (_commandTerm = GetCommandTerm()); }
        }

        public IEnumerable<CommandTermBase> NextCommandTerms { get; set; }

        internal int MaxOptionalDepth
        {
            get { return GetMaxOptionalDepth(); }
        }

        internal int MaxRequierdDepth
        {
            get { return GetMaxRequiredDepth(); }
        }

        protected virtual int GetMaxOptionalDepth()
        {
            return 1;
        }

        protected virtual int GetMaxRequiredDepth()
        {
            return 1;
        }

        protected abstract string GetCommandTerm();

//        protected abstract IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params);

        /// <summary>
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return (NextCommandTerms ?? new CommandTermBase[] {}).GetEnumerator();
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
        /// ���������� ������, ������� ������������ ������� ������.
        /// </summary>
        /// <returns>
        /// ������, �������������� ������� ������.
        /// </returns>
        public override string ToString()
        {
            return CommandTerm;
        }
    }
}