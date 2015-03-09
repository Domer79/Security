using System;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Extensions;

namespace DataRepository.Exceptions
{
    public class RepositoryValidationException : Exception
    {
        public RepositoryValidationException(string message, params object[] args)
            : base(string.Format("Message: {0}, additional args: {1}", message, args.SplitReverse()))
        {
                
        }
    }
}