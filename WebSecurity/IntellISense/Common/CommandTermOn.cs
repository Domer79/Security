using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Common
{
    internal class CommandTermOn : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "on";
        }
    }
}