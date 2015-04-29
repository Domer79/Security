using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermMemberName : CommandTermBase
    {
        public CommandTermMemberName(string memberName)
        {
            MemberName = memberName;
        }

        protected override string GetCommandTerm()
        {
            return MemberName;
        }

        public string MemberName { get; set; }
    }
}