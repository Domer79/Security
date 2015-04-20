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
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _list.GetEnumerator();
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
        /// Добавляет элемент в коллекцию <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">Объект, добавляемый в коллекцию <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">Объект <see cref="T:System.Collections.Generic.ICollection`1"/> доступен только для чтения.</exception>
        [Obsolete("Не поддерживается. Используйте Add(string commandString)", true)]
        void ICollection<CommandTermBase>.Add(CommandTermBase item)
        {
            throw new NotSupportedException("Не поддерживается. Используйте Add(string commandString)");
        }

        /// <summary>
        /// Ищет CommandTerm в следующих командах последнего CommandTerm и добавляет его в стек
        /// </summary>
        /// <param name="commandString">Имя команды</param>
        public void Add(string commandString)
        {
            if (string.IsNullOrEmpty(commandString)) 
                throw new ArgumentNullException("commandString");

            if (!CanAdd(commandString))
                throw new InvalidOperationException(string.Format("Данное слово {0} недопустимо", commandString));

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
        /// Удаляет все элементы из коллекции <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">Объект <see cref="T:System.Collections.Generic.ICollection`1"/> доступен только для чтения.</exception>
        [Obsolete("Не поддерживается", true)]
        void ICollection<CommandTermBase>.Clear()
        {
            throw new NotSupportedException("Не поддерживается");
        }

        /// <summary>
        /// Определяет, содержит ли коллекция <see cref="T:System.Collections.Generic.ICollection`1"/> указанное значение.
        /// </summary>
        /// <returns>
        /// Значение true, если параметр <paramref name="item"/> найден в коллекции <see cref="T:System.Collections.Generic.ICollection`1"/>; в противном случае — значение false.
        /// </returns>
        /// <param name="item">Объект, который требуется найти в <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        [Obsolete("Не поддерживается", true)]
        bool ICollection<CommandTermBase>.Contains(CommandTermBase item)
        {
            throw new NotSupportedException("Не поддерживается");
        }

        /// <summary>
        /// Копирует элементы <see cref="T:System.Collections.Generic.ICollection`1"/> в массив <see cref="T:System.Array"/>, начиная с указанного индекса <see cref="T:System.Array"/>.
        /// </summary>
        /// <param name="array">Одномерный массив <see cref="T:System.Array"/>, в который копируются элементы из интерфейса <see cref="T:System.Collections.Generic.ICollection`1"/>. Массив <see cref="T:System.Array"/> должен иметь индексацию, начинающуюся с нуля.</param><param name="arrayIndex">Отсчитываемый от нуля индекс в массиве <paramref name="array"/>, указывающий начало копирования.</param><exception cref="T:System.ArgumentNullException">Параметр <paramref name="array"/> имеет значение null.</exception><exception cref="T:System.ArgumentOutOfRangeException">Значение параметра <paramref name="arrayIndex"/> меньше 0.</exception><exception cref="T:System.ArgumentException">Количество элементов в исходной коллекции <see cref="T:System.Collections.Generic.ICollection`1"/> превышает доступное место, начиная с индекса <paramref name="arrayIndex"/> до конца массива назначения <paramref name="array"/>.</exception>
        public void CopyTo(CommandTermBase[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Удаляет первый экземпляр указанного объекта из коллекции <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// Значение true, если элемент <paramref name="item"/> успешно удален из <see cref="T:System.Collections.Generic.ICollection`1"/>, в противном случае — значение false. Этот метод также возвращает значение false, если параметр <paramref name="item"/> не найден в исходном интерфейсе <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">Объект, который необходимо удалить из коллекции <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">Объект <see cref="T:System.Collections.Generic.ICollection`1"/> доступен только для чтения.</exception>
        [Obsolete("Не поддерживается", true)]
        bool ICollection<CommandTermBase>.Remove(CommandTermBase item)
        {
            throw new NotSupportedException("Не поддерживается");
        }

        /// <summary>
        /// Получает число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// Число элементов, содержащихся в интерфейсе <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// Получает значение, указывающее, доступна ли <see cref="T:System.Collections.Generic.ICollection`1"/> только для чтения.
        /// </summary>
        /// <returns>
        /// Значение true, если <see cref="T:System.Collections.Generic.ICollection`1"/> доступна только для чтения; в противном случае — значение false.
        /// </returns>
        bool ICollection<CommandTermBase>.IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Играет роль хэш-функции для определенного типа.
        /// </summary>
        /// <returns>
        /// Хэш-код для текущего объекта <see cref="T:System.Object"/>.
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