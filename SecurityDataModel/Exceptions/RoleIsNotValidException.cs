using System;

namespace SecurityDataModel.Exceptions
{
    public class RoleIsNotValidException : Exception
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.Exception"/>.
        /// </summary>
        public RoleIsNotValidException()
        {
        }

        /// <summary>
        /// ��������� ������������� ������ ���������� ������ <see cref="T:System.Exception"/>, ��������� ��������� ��������� �� ������.
        /// </summary>
        /// <param name="message">���������, ����������� ������.</param>
        public RoleIsNotValidException(string message) 
            : base(message)
        {
        }
    }
}