using System;
using System.Runtime.Serialization;
using SystemTools.Extensions;

namespace SecurityDataModel.Exceptions
{
    public class RoleNotFoundException : BaseException
    {
        public RoleNotFoundException(params object[] args)
            : base(string.Format("���� �� �������. ���������: {0}", args.SplitReverse()))
        {
        }
    }
}