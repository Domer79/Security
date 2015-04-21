using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermUserName : CommandTermBase
    {
        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }

        protected override string GetCommandTerm()
        {
            return UserName;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public string UserName { get; set; }
    }
}