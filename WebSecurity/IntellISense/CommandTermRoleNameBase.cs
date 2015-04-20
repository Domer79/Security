using System.Collections.Generic;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense
{
    internal abstract class CommandTermRoleNameBase : CommandTermBase
    {
        public string RoleName { get; set; }

        protected sealed override string GetCommandTerm()
        {
            return RoleName;
        }

        protected override abstract IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params);
    }
}