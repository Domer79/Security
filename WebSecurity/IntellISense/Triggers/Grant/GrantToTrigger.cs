using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Grant.AccessTypes;
using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Triggers.Grant
{
    public class GrantToTrigger : ICommandTermTrigger
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
                    },
                    new[]
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                    },

                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.GrantTrigger; }
        }
    }
}
