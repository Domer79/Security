using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermExec : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "exec";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return NextCommandTermList;
        }

        public CommandTermBase NextCommandTermList { get; set; }
    }
}