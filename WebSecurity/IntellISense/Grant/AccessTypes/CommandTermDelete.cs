using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Grant.AccessTypes
{
    internal class CommandTermDelete : CommandTermAccessTypeBase
    {
        public CommandTermDelete()
            : base(1)
        {
        }

        public CommandTermDelete(int depth) 
            : base(depth)
        {
        }

        protected override string GetCommandTerm()
        {
            return "delete";
        }
    }
}