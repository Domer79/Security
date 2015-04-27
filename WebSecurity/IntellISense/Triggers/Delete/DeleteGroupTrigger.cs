using System;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Delete;

namespace WebSecurity.IntellISense.Triggers.Delete
{
    public class DeleteGroupTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof(CommandTermDelete),
                        typeof(CommandTermCommonGroup)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteGroupTrigger; }
        }
    }

    public class DeleteControllerTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof (CommandTermDelete),
                        typeof (CommandTermCommonController)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteControllerTrigger; }
        }
    }

    public class DeleteTableTrigger : ICommandTermTrigger
    {
        public Type[][] CommandTermTypes
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        typeof (CommandTermDelete),
                        typeof (CommandTermCommonTable)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteTableTrigger; }
        }
    }
}
