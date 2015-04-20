using System.Collections.Generic;
using WebSecurity.IntellISense.Base;
using WebSecurity.IntellISense.Grant.AccessTypes;

namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermRoleName : CommandTermRoleNameBase
    {
        private readonly CommandTermOnGrant _commandTermOn = new CommandTermOnGrant();

        protected internal override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermOn;
        }
    }
}