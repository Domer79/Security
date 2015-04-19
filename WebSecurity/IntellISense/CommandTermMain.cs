using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSecurity.IntellISense.Add;
using WebSecurity.IntellISense.Grant;

namespace WebSecurity.IntellISense
{
    internal class CommandTermMain : CommandTermBase
    {
        private readonly CommandTermGrant _commandTermGrant = new CommandTermGrant();

        protected override string GetCommandTerm()
        {
            return null;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermGrant;
        }
    }
}
