using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;

namespace WebSecurity.IntellISense.Set
{
    public class CommandTermSet : CommandTermBase
    {
        private readonly CommandTermBase _commandTermRole = new CommandTermCommonRole();
        private readonly CommandTermBase _commandTermGroup = new CommandTermCommonGroup();
        private readonly CommandTermBase _commandTermPassword = new CommandTermPassword();

        protected override string GetCommandTerm()
        {
            return "set";
        }

        public CommandTermSet()
        {
            NextCommandTerms = new List<CommandTermBase>(GetNextCommandTerms());
        }

        private IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermRole;
            yield return _commandTermGroup;
            yield return _commandTermPassword;
        }
    }
}
