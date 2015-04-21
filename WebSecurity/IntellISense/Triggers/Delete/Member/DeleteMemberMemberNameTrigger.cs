using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Delete;

namespace WebSecurity.IntellISense.Triggers.Delete.Member
{
    public class DeleteMemberMemberNameTrigger : ICommandTermTrigger
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
                        typeof(CommandTermMemberName)
                    }
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return TriggerActions.DeleteMemberMemberName; }
        }
    }
}
