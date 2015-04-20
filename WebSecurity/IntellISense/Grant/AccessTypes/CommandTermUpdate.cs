using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermUpdate : CommandTermAccessTypeBase
    {
        public CommandTermUpdate()
            : base(1)
        {
        }

        public CommandTermUpdate(int depth) 
            : base(depth)
        {
        }

        protected override string GetCommandTerm()
        {
            return "update";
        }
    }
}