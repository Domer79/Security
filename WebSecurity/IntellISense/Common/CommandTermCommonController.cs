using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Common
{
    internal class CommandTermCommonController : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "controller";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}