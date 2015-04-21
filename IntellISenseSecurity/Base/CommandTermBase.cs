using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntellISenseSecurity.Base
{
    public abstract class CommandTermBase : IEnumerable<CommandTermBase>
    {
        private string _commandTerm;
        private List<CommandTermBase> _nextCommandTerms;

        public string CommandTerm
        {
            get { return _commandTerm ?? (_commandTerm = GetCommandTerm()); }
        }

        private IEnumerable<CommandTermBase> NextCommandTerms
        {
            get
            {
                if (_nextCommandTerms != null)
                    return _nextCommandTerms;

                var nextCommandTerms = GetNextCommandTerms();
//                if (nextCommandTerms == null)
//                    return null;

                var commandTerms = nextCommandTerms == null ? new CommandTermBase[]{}: nextCommandTerms.ToArray();
//                if (!commandTerms.Any())
//                    return null;

//                return _nextCommandTerms ?? (_nextCommandTerms = new List<CommandTermBase>(commandTerms));
                return _nextCommandTerms ?? (_nextCommandTerms = new List<CommandTermBase>(commandTerms));
            }
        }

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

        protected abstract IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params);

        /// <summary>
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return NextCommandTerms.GetEnumerator();
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