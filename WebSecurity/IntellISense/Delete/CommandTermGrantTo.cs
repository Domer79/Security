using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity.Base;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense.Delete
{
    internal class CommandTermGrantTo : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "grant to";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return RoleRepository.GetRoleCollection().Select(r => new CommandTermRoleName(r.RoleName));
        }
    }
}
