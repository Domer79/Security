using System;
using System.Runtime.Serialization;

namespace SecurityDataModel.Exceptions
{
    public class UserGroupExistsException : BaseException
    {
        public UserGroupExistsException()
        {
        }

        public UserGroupExistsException(string message) 
            : base(message)
        {
        }

        public UserGroupExistsException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        public UserGroupExistsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public UserGroupExistsException(string message, params object[] args) 
            : base(message, args)
        {
        }
    }
}