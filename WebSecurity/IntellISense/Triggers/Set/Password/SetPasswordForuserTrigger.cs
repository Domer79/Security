using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity;
using WebSecurity.IntellISense.Common;
using WebSecurity.IntellISense.Set;

namespace WebSecurity.IntellISense.Triggers.Set.Password
{
    public class SetPasswordForUserTrigger : ICommandTermTrigger
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
                        typeof (CommandTermPassword),
                        typeof (CommandTermAdditionalParam),
                        typeof (CommandTermFor),
                        typeof (CommandTermCommonUser)
                    }
                };
            }
        }

        public Action<CommandTermStack> Trigger
        {
            get { return TriggerActions.SetPasswordForUserTrigger; }
        }
    }
}
