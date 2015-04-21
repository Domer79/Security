using System;
using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Grant;

namespace WebSecurity.IntellISense.CommandTermCommon
{
    internal class CommandTermCommonGroup : CommandTermBase
    {
        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }

        protected override string GetCommandTerm()
        {
            return "group";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }
    }
}