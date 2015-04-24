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
            //TODO: Преобразовать в выражение LINQ, после прохождения всех тестов
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