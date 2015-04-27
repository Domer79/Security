using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Set;

namespace WebSecurity.IntellISense.Triggers.Set.Group
{
    public class SetGroupToTrigger : ICommandTermTrigger
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
                        typeof (CommandTermCommonGroup),
                        typeof (CommandTermGroupName),
                        typeof (CommandTermTo)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.SetGroupToTrigger; }
        }
    }
}