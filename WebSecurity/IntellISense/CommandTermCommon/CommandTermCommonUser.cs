using System;
using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.CommandTermCommon
{
    internal class CommandTermCommonUser : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "user";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}