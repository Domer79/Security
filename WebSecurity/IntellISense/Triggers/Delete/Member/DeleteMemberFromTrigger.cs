using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Delete;

namespace WebSecurity.IntellISense.Triggers.Delete.Member
{
    public class DeleteMemberFromTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof(CommandTermDelete),
                        typeof(CommandTermMember),
                        typeof(CommandTermMemberName),
                        typeof(CommandTermFrom)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteMemberFromTrigger; }
        }
    }
}
