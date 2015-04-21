using System.Collections.Generic;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Add;
using WebSecurity.IntellISense.Delete;
using WebSecurity.IntellISense.Grant;
using WebSecurity.IntellISense.Set;

namespace WebSecurity.IntellISense
{
    public class CommandTermMain : CommandTermEntryPoint
    {
        private readonly CommandTermGrant _commandTermGrant = new CommandTermGrant();
        private readonly CommandTermAdd _commandTermAdd = new CommandTermAdd();
        private readonly CommandTermBase _commandTermSet = new CommandTermSet();
        private readonly CommandTermBase _commandTermDelete = new CommandTermDelete();

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermGrant;
            yield return _commandTermAdd;
            yield return _commandTermSet;
            yield return _commandTermDelete;
        }

        public override ICommandTermTrigger[] Triggers
        {
//            get { return new ICommandTermTrigger[] {new ExecTrigger(), new TableTrigger(), new GrantToTrigger(), }; }
            get { return base.Triggers; }
        }
    }
}