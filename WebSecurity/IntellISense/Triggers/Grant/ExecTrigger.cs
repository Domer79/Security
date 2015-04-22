using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Grant.AccessTypes;

namespace WebSecurity.IntellISense.Triggers.Grant
{
    public class ExecTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermExec),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOnGrant)
                    }
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return TriggerActions.GrantExecTrigger; }
        }
    }
}
