using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermExec : CommandTermBase
    {
        public CommandTermExec()
        {
            NextCommandTerms = new List<CommandTermBase>{new CommandTermTo()};
        }

        protected override string GetCommandTerm()
        {
            return "exec";
        }
    }
}