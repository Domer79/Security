using System;

namespace SecurityDataModel.Exceptions
{
    public class GroupExistsException : Exception
    {
        public GroupExistsException()
            : base("Группа уже существует в базе данных.")
        {
        }

        public GroupExistsException(string message) 
            : base(message)
        {
        }
    }
}