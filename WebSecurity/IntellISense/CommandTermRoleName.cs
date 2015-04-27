using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    internal class CommandTermRoleName : CommandTermBase
    {
        public string RoleName { get; set; }

        public CommandTermRoleName()
        {
        }

        public CommandTermRoleName(string roleName)
        {
            RoleName = roleName;
        }

        protected sealed override string GetCommandTerm()
        {
            return RoleName;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}