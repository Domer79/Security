using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.Models;
using WebSecurity.Data;
using WebSecurity.IntellISense.Base;
using WebSecurity.IntellISense.Grant.AccessTypes;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense.Grant.Triggers
{
    public class ExecTrigger : ICommandTermTrigger
    {
        public Type[] CommandTermTypes
        {
            get
            {
                return new []
                {
                    typeof(CommandTermGrant),
                    typeof(CommandTermExec),
                    typeof(CommandTermTo),
                    typeof(CommandTermGrantRoleName),
                    typeof(CommandTermOnGrant)
                };
            }
        }

        public Action<CommandTermBase> Trigger
        {
            get { return DoTrigger; }
        }

        private void DoTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermOnGrant == null) 
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermOnGrant;
            ct.SecObjectCollection = new ActionResultRepository().GetQueryableCollection();
        }
    }
}
