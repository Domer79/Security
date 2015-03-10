using System;
using System.Runtime.Serialization;
using SystemTools.Extensions;

namespace SecurityDataModel.Exceptions
{
    public class RoleNotFoundException : BaseException
    {
        public RoleNotFoundException(params object[] args)
            : base(string.Format("Роль не найдена. Аргументы: {0}", args.SplitReverse()))
        {
        }
    }
}