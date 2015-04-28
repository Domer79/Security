using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermMemberName : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return MemberName;
        }

        public string MemberName { get; set; }
    }
}