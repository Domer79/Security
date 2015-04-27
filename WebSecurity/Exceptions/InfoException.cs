using System;

namespace WebSecurity.Exceptions
{
    internal class InfoException : Exception
    {
        public InfoException(string message)
            : base(message)
        {
        }
    }
}