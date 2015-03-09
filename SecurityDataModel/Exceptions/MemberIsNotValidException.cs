using System;

namespace SecurityDataModel.Exceptions
{
    public class MemberIsNotValidException : Exception
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.Exception"/>.
        /// </summary>
        public MemberIsNotValidException()
        {
        }

        /// <summary>
        /// ��������� ������������� ������ ���������� ������ <see cref="T:System.Exception"/>, ��������� ��������� ��������� �� ������.
        /// </summary>
        /// <param name="message">���������, ����������� ������.</param>
        public MemberIsNotValidException(string message) 
            : base(message)
        {
        }
    }
}