using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Common
{
    internal class CommandTermCommonController : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "controller";
        }
    }
}