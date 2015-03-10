using System;
using SystemTools.Extensions;

namespace SecurityDataModel.Exceptions
{
    public class MemberExistsException : Exception
    {
        public MemberExistsException(params object[] args)
            : base(string.Format("Участник безопасности уже существует в базе данных: {0}.", args.SplitReverse()))
        {
        }

        public MemberExistsException(string message) 
            :base(message)
        {
        }
    }
}