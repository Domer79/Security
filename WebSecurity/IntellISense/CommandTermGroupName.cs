using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermGroupName : CommandTermBase
    {
        public CommandTermGroupName(string groupName)
        {
            GroupName = groupName;
        }

        protected override string GetCommandTerm()
        {
            return GroupName;
        }

        public string GroupName { get; set; }
    }
}