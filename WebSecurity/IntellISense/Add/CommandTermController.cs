using System;
using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Add
{
    internal class CommandTermController : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "controller";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return null;
        }
    }
}