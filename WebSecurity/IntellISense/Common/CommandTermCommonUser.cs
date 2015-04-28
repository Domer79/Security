using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Common
{
    internal class CommandTermCommonUser : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "user";
        }
    }
}