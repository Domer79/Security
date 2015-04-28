using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Triggers.Grant
{
    public class TableTrigger : ICommandTermTrigger
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
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOn)
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOn)
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOn)
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOn)
                    },
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DoTableTrigger; }
        }
    }
}
