using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermGroupName : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return GroupName;
        }

        public string GroupName { get; set; }
    }
}