using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Set;

namespace WebSecurity.IntellISense.Triggers.Set.Role
{
    public class SetRoleRoleNameTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof (CommandTermSet),
                        typeof (CommandTermCommonRole),
                        typeof (CommandTermRoleName)
                    }
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return TriggerActions.SetRoleRoleNameTrigger; }
        }
    }
}