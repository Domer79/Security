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

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public string MemberName { get; set; }
        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}