using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    internal class CommandTermRoleName : CommandTermBase
    {
        public string RoleName { get; set; }

        public CommandTermRoleName(string roleName)
        {
            RoleName = roleName;
        }

        protected sealed override string GetCommandTerm()
        {
            return RoleName;
        }
    }
}