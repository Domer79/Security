using System;

namespace SecurityDataModel.Exceptions
{
    public class GroupExistsException : Exception
    {
        public GroupExistsException()
            : base("������ ��� ���������� � ���� ������.")
        {
        }

        public GroupExistsException(string message) 
            : base(message)
        {
        }
    }
}