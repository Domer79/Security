namespace WebSecurity.IntellISense.Grant
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