using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermExec : CommandTermBase
    {
        private readonly CommandTermTo _commandTermTo = new CommandTermTo();

        protected override string GetCommandTerm()
        {
            return "exec";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermTo;
        }
    }
}