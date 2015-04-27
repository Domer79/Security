using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Set;

namespace WebSecurity.IntellISense.Triggers.Set.Role
{
    public class SetRoleTrigger : ICommandTermTrigger
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
                        typeof (CommandTermCommonRole)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.SetRoleTrigger; }
        }
    }
}
