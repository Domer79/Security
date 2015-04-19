namespace WebSecurity.IntellISense.Grant
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