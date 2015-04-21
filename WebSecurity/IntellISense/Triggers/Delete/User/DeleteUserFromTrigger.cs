using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Delete;

namespace WebSecurity.IntellISense.Triggers.Delete.User
{
    public class DeleteUserFromTrigger : ICommandTermTrigger
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
                        typeof(CommandTermCommonUser),
                        typeof(CommandTermUserName),
                        typeof(CommandTermFrom)
                    }
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return TriggerActions.DeleteUserFromTrigger; }
        }
    }
}
