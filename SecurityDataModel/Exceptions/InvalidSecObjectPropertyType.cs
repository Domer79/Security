using System;

namespace SecurityDataModel.Exceptions
{
    internal class InvalidSecObjectPropertyType : Exception
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.Exception"/>.
        /// </summary>
        public InvalidSecObjectPropertyType(string propertyName)
            : base(string.Format("��� �������� {0}, ������ ���� string", propertyName))
        {
        }
    }
}