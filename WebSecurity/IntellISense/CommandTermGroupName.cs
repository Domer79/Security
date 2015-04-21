using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermGroupName : CommandTermBase
    {
        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }

        protected override string GetCommandTerm()
        {
            return GroupName;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public string GroupName { get; set; }
    }
}