using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Add
{
    internal class CommandTermAllSecurityObjects : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "allsecurityobjects";
        }
    }
}