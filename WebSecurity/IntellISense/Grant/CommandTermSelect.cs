namespace WebSecurity.IntellISense.Grant
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