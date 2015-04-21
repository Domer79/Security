using System;
using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Add
{
    internal class CommandTermTable : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "table";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return null;
        }
    }
}