using System;
using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.CommandTermCommon
{
    internal class CommandTermCommonRole : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "role";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}