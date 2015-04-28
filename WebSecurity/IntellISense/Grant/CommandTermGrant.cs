using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Grant.AccessTypes;
using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Grant
{
    //TODO: Написать триггер после окончания каждого AccessType
    public class CommandTermGrant : CommandTermBase
    {
        private readonly CommandTermBase _commandTermExec = new CommandTermExec();
        private readonly CommandTermAccessTypeBase _commandTermSelect = new CommandTermSelect();
        private readonly CommandTermAccessTypeBase _commandTermInsert = new CommandTermInsert();
        private readonly CommandTermAccessTypeBase _commandTermUpdate = new CommandTermUpdate();
        private readonly CommandTermAccessTypeBase _commandTermDelete = new CommandTermDelete();

        public CommandTermGrant()
        {
            NextCommandTerms = new List<CommandTermBase>(GetNextCommandTerms());
        }

        protected override string GetCommandTerm()
        {
            return "grant";
        }

        private IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermExec;
            yield return _commandTermSelect;
            yield return _commandTermInsert;
            yield return _commandTermUpdate;
            yield return _commandTermDelete;
        }
    }
}