using System.Collections.Generic;
using System.Linq;
using IntellISenseSecurity.Base;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense.Delete
{
    internal class CommandTermMember : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "member";
        }
    }
}