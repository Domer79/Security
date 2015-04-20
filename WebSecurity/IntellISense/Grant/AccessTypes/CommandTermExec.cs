using System.Collections.Generic;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermExec : CommandTermBase
    {
        private readonly CommandTermTo _commandTermTo = new CommandTermTo();

        protected override string GetCommandTerm()
        {
            return "exec";
        }

        protected internal override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermTo;
        }
    }
}