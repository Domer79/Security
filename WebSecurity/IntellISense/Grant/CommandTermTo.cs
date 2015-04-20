using System.Collections.Generic;
using WebSecurity.IntellISense.Base;
using WebSecurity.IntellISense.Grant.AccessTypes;

namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermTo : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "to";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return new RoleNameCommandTermList<CommandTermGrantRoleName>();
        }
    }
}