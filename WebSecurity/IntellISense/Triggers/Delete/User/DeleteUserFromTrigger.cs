﻿using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;
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
                        typeof(CommandTermRemove),
                        typeof(CommandTermCommonUser),
                        typeof(CommandTermUserName),
                        typeof(CommandTermFrom)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteUserFromTrigger; }
        }
    }
}
