﻿using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Grant.AccessTypes.Base;

namespace WebSecurity.IntellISense.Triggers
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
                        typeof (CommandTermOnGrant)
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOnGrant)
                    },
                    new []
                    {
                        typeof (CommandTermGrant),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermAccessTypeBase),
                        typeof (CommandTermTo),
                        typeof (CommandTermRoleName),
                        typeof (CommandTermOnGrant)
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
                        typeof (CommandTermOnGrant)
                    },
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return TriggerActions.DoTableTrigger; }
        }
    }
}