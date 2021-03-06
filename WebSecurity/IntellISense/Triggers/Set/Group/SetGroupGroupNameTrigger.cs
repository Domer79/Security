﻿using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;
using WebSecurity.IntellISense.Set;

namespace WebSecurity.IntellISense.Triggers.Set.Group
{
    public class SetGroupGroupNameTrigger : ICommandTermTrigger
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
                        typeof (CommandTermGroupName)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.SetGroupGroupNameTrigger; }
        }
    }
}