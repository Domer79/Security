using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Delete;

namespace WebSecurity.IntellISense.Triggers.Delete.User
{
    public class DeleteUserTrigger : ICommandTermTrigger
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
                        typeof(CommandTermCommonUser)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.DeleteUserTrigger; }
        }
    }
}
