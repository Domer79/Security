using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebSecurity.IntellISense
{
    public class CommandTermDispatcher : IEnumerable<string>
    {
        private const string CommandStringPattern = @"(?<term>[^\W]+)(?<delimiter>[^\w]*)";
        private const string EndSpacePattern = @"[^\w]$";
        private readonly CommandTermMain _commandTermMain = new CommandTermMain();

        public CommandTermDispatcher(string command)
        {
            CommandStrings = GetCommandStrings(command);
        }

        public string[] CommandStrings { get; private set; }

        private CommandTermMain CommandTermMain
        {
            get { return _commandTermMain; }
        }

        private string[] GetCommandStrings(string command)
        {
            var rx = new Regex(CommandStringPattern);
            var commandStrings = rx.Matches(command).OfType<Match>().Select(m => m.Groups["term"].Value).Union(GetEndSpace(command)).ToArray();
            return commandStrings.Length == 0 ? new []{""} : commandStrings;
        }

        private IEnumerable<string> GetEndSpace(string command)
        {
            return Regex.IsMatch(command, EndSpacePattern) ? new[] {""} : new string[] {};
        }

        private IEnumerable<string> GetCommandTerms()
        {
            return GetCommandTerms(0, CommandTermMain);
        }

        private IEnumerable<string> GetCommandTerms(int termIndex, CommandTermBase commandTerm)
        {
            if (termIndex == CommandStrings.Length - 1)
                return commandTerm.GetNextCommandTerms().Where(t => t.CommandTerm.StartsWith(CommandStrings[termIndex])).Select(t => t.ToString());

            return GetCommandTerms(termIndex + 1, commandTerm.GetNextCommandTerms().First(t => t.CommandTerm == CommandStrings[termIndex]));
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