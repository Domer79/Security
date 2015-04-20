using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermSelect : CommandTermAccessTypeBase
    {
        public CommandTermSelect()
            : base(1)
        {
        }

        public CommandTermSelect(int depth) 
            : base(depth)
        {
        }

        protected override string GetCommandTerm()
        {
            return "select";
        }
    }
}