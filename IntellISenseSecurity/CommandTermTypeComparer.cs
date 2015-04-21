using System;
using System.Collections.Generic;
using SystemTools.Extensions;

namespace IntellISenseSecurity
{
    internal class CommandTermTypeComparer : IEqualityComparer<Type>
    {
        /// <summary>
        /// Определяет, равны ли два указанных объекта.
        /// </summary>
        /// <returns>
        /// true, если указанные объекты равны; в противном случае — false.
        /// </returns>
        /// <param name="x">Первый сравниваемый объект типа <paramref name="Type"/>.</param><param name="y">Второй сравниваемый объект типа <paramref name="Type"/>.</param>
        public bool Equals(Type x, Type y)
        {
            return x.Is(y) || y.Is(x);
        }

        /// <summary>
        /// Возвращает хэш-код указанного объекта.
        /// </summary>
        /// <returns>
        /// Хэш-код указанного объекта.
        /// </returns>
        /// <param name="obj">Объект <see cref="T:System.Object"/>, для которого необходимо возвратить хэш-код.</param><exception cref="T:System.ArgumentNullException">Тип параметра <paramref name="obj"/> является ссылочным типом и значение параметра <paramref name="obj"/> — null.</exception>
        public int GetHashCode(Type obj)
        {
            return obj.GetHashCode();
        }
    }
}