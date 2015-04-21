using System;
using System.Collections.Generic;
using SystemTools.Extensions;

namespace IntellISenseSecurity
{
    internal class CommandTermTypeComparer : IEqualityComparer<Type>
    {
        /// <summary>
        /// ����������, ����� �� ��� ��������� �������.
        /// </summary>
        /// <returns>
        /// true, ���� ��������� ������� �����; � ��������� ������ � false.
        /// </returns>
        /// <param name="x">������ ������������ ������ ���� <paramref name="Type"/>.</param><param name="y">������ ������������ ������ ���� <paramref name="Type"/>.</param>
        public bool Equals(Type x, Type y)
        {
            return x.Is(y) || y.Is(x);
        }

        /// <summary>
        /// ���������� ���-��� ���������� �������.
        /// </summary>
        /// <returns>
        /// ���-��� ���������� �������.
        /// </returns>
        /// <param name="obj">������ <see cref="T:System.Object"/>, ��� �������� ���������� ���������� ���-���.</param><exception cref="T:System.ArgumentNullException">��� ��������� <paramref name="obj"/> �������� ��������� ����� � �������� ��������� <paramref name="obj"/> � null.</exception>
        public int GetHashCode(Type obj)
        {
            return obj.GetHashCode();
        }
    }
}