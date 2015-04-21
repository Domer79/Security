using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IntellISenseSecurity.Base;

namespace IntellISenseSecurity
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
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            return GetCommandTerms().GetEnumerator();
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

        #endregion
    }
}