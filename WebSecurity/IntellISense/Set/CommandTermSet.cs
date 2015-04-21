using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;

namespace WebSecurity.IntellISense.Set
{
    public class CommandTermSet : CommandTermBase
    {
        private readonly CommandTermBase _commandTermRole = new CommandTermCommonRole();
        private readonly CommandTermBase _commandTermGroup = new CommandTermCommonGroup();

        protected override string GetCommandTerm()
        {
            return "set";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermRole;
            yield return _commandTermGroup;
        }
    }
}
