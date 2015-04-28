using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTermUserName : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return UserName;
        }
        public string UserName { get; set; }
    }
}