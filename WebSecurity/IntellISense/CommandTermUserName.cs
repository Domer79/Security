using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermUserName : CommandTermBase
    {
        public CommandTermUserName(string userName)
        {
            UserName = userName;
        }

        protected override string GetCommandTerm()
        {
            return UserName;
        }
        public string UserName { get; set; }
    }
}