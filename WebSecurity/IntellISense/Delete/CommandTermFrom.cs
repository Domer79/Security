using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Delete
{
    internal class CommandTermFrom : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "from";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}