using System;
using System.Runtime.Serialization;

namespace SecurityDataModel.Exceptions
{
    internal class AccessTypeDeleteException : BaseException
    {
        public AccessTypeDeleteException()
        {
        }

        public AccessTypeDeleteException(string message) 
            : base(message)
        {
        }

        public AccessTypeDeleteException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected AccessTypeDeleteException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public AccessTypeDeleteException(string message, params object[] args) 
            : base(message, args)
        {
        }
    }
}