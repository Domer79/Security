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
//        private const string CommandStringPattern = @"[^\s]+";
        private const string CommandStringPattern = @"(""[\w\s]+"")|[^\s]+";
        private const string EndSpacePattern = @"[\s]$";
        private CommandTermStack _stack;

        public CommandTermDispatcher(string command)
        {
            CommandStrings = GetCommandStrings(command);
        }

        private string[] CommandStrings { get; set; }

        public CommandTermStack Stack
        {
            get
            {
                if (_stack == null)
                    StackInit();

                return _stack;
            }
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
//            return GetCommandTerms(0, new TCommandTermEntryPoint());
//            return GetCommandTerms2(0);
            return GetCommandTerms3();
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
            if (!Stack.CanAdd(term))
            {
                var commandTerms2 = Stack.LastCommandNext().Where(t => t.CommandTerm.ToLower().Contains(term.ToLower())).Select(t => t.ToString());
                for (var i = termIndex; i < CommandStrings.Length; i++)
                {
                    Stack.AddAdditionCommandTerm(CommandStrings[i]);
                }
                return commandTerms2;
            }

            Stack.Add(term);
            return GetCommandTerms2(termIndex + 1);
        }

        private IEnumerable<string> GetCommandTerms3()
        {
//            var lastCommandsNextNotAdditional = Stack.GetLastCommandsNextNotAdditional();
            var lastCommandsNextNotAdditional = Stack[Stack.Count - 2];

//            if (lastCommandsNextNotAdditional.ToString() == LastTerm)
//                return new string[] {};

            return lastCommandsNextNotAdditional.Where(t => t.CommandTerm.ToLower().Contains(LastTerm.ToLower())).Select(t => t.ToString());
        }

        public string LastTerm
        {
            get { return CommandStrings[CommandStrings.Length - 1]; }
        }

        private void StackInit()
        {
            _stack = new CommandTermStack(new TCommandTermEntryPoint());

            foreach (var commandString in CommandStrings)
            {
                if (!Stack.CanAdd(commandString))
                    Stack.AddAdditionCommandTerm(commandString);
                else
                    Stack.Add(commandString);
            }
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