using System;
using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Grant;

namespace WebSecurity.IntellISense.CommandTermCommon
{
    internal class CommandTermTo : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "to";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }

        //TODO: Реализовать SetTrigger
    }
}