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
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return (NextCommandTerms ?? new CommandTermBase[] {}).GetEnumerator();
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

        /// <summary>
        /// Возвращает строку, которая представляет текущий объект.
        /// </summary>
        /// <returns>
        /// Строка, представляющая текущий объект.
        /// </returns>
        public override string ToString()
        {
            return CommandTerm;
        }
    }
}