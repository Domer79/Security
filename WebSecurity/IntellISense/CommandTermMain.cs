using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSecurity.IntellISense.Add;
using WebSecurity.IntellISense.Base;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Grant.Triggers;

namespace WebSecurity.IntellISense
{
    public class CommandTermMain : CommandTermEntryPoint
    {
        private readonly CommandTermGrant _commandTermGrant = new CommandTermGrant();

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermGrant;
        }

        public override ICommandTermTrigger[] Triggers
        {
            get { return new ICommandTermTrigger[] {new ExecTrigger(),}; }
        }
    }
}