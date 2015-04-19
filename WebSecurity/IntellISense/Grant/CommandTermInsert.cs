namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermInsert : CommandTermAccessTypeBase
    {
        public CommandTermInsert()
            : base(1)
        {
        }

        public CommandTermInsert(int depth) 
            : base(depth)
        {
        }

        protected override string GetCommandTerm()
        {
            return "insert";
        }
    }
}