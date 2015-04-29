using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;
using WebSecurity.IntellISense.Grant;

namespace WebSecurity.IntellISense.Delete
{
    public class CommandTermRemove : CommandTermBase
    {
        private readonly CommandTermBase _commandTermMember = new CommandTermMember();
        private readonly CommandTermBase _commandTermUser = new CommandTermCommonUser();
        private readonly CommandTermBase _commandTermGroup = new CommandTermCommonGroup();
        private readonly CommandTermBase _commandTermController = new CommandTermCommonController();
        private readonly CommandTermBase _commandTermTable = new CommandTermCommonTable();
        private readonly CommandTermBase _commandTermGrant = new CommandTermGrant();

        protected override string GetCommandTerm()
        {
            return "remove";
        }

        public CommandTermRemove()
        {
            NextCommandTerms = new List<CommandTermBase>(GetNextCommandTerms());
        }

        private IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermMember;
            yield return _commandTermUser;
            yield return _commandTermGroup;
            yield return _commandTermController;
            yield return _commandTermTable;
            yield return _commandTermGrant;
        }
    }
}
