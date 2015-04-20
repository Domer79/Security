using System.Collections.Generic;

namespace WebSecurity.IntellISense.Base
{
    public abstract class CommandTermEntryPoint : CommandTermBase
    {
        protected sealed override string GetCommandTerm()
        {
            return null;
        }

        protected override abstract IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params);

        public virtual ICommandTermTrigger[] Triggers
        {
            get { return new ICommandTermTrigger[]{}; }
        }
    }
}