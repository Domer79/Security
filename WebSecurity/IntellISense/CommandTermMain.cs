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

        public CommandTermMain()
        {
            NextCommandTerms = new List<CommandTermBase>
            {
                _commandTermGrant,
                _commandTermAdd,
                _commandTermSet,
                _commandTermDelete
            };
        }
    }
}