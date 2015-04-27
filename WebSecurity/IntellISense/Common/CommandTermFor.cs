using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Common
{
    internal class CommandTermFor : CommandTermBase
    {
        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }

        protected override string GetCommandTerm()
        {
            return "for";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }
    }
}