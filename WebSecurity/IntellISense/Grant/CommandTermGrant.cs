using System.Collections.Generic;

namespace WebSecurity.IntellISense.Grant
{
    public class CommandTermGrant : CommandTermBase
    {
        private readonly CommandTermAccessTypeBase _commandTermExec = new CommandTermExec();
        private readonly CommandTermAccessTypeBase _commandTermSelect = new CommandTermSelect();
        private readonly CommandTermAccessTypeBase _commandTermInsert = new CommandTermInsert();
        private readonly CommandTermAccessTypeBase _commandTermUpdate = new CommandTermUpdate();
        private readonly CommandTermAccessTypeBase _commandTermDelete = new CommandTermDelete();

        protected override string GetCommandTerm()
        {
            return "grant";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermExec;
            yield return _commandTermSelect;
            yield return _commandTermInsert;
            yield return _commandTermUpdate;
            yield return _commandTermDelete;
        }
    }
}