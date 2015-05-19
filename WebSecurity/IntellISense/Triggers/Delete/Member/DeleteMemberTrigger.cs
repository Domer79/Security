using System;
using IntellISenseSecurity;
using WebSecurity.IntellISense.Delete;

namespace WebSecurity.IntellISense.Triggers.Delete.Member
{
    public class DeleteMemberTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof(CommandTermRemove),
                        typeof(CommandTermMember)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteMemberTrigger; }
        }
    }
}