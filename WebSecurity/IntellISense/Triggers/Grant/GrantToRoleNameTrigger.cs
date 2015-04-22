using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Grant.AccessTypes;
using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Triggers.Grant
{
    public class GrantToRoleNameTrigger : ICommandTermTrigger
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
                    },
                    new[]
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                    },
                    new[]
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                    },
                    new[]
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                    },
                    new[]
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                    },
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return TriggerActions.GrantToRoleNameTrigger; }
        }
    }
}
