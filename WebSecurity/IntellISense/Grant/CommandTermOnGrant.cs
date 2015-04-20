using System.Collections.Generic;
using WebSecurity.IntellISense.Base;
using WebSecurity.IntellISense.Grant.AccessTypes;

namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermOnGrant : CommandTermBase
    {
        private readonly SecObjectCommandTermList _secObjectCommandTermList = new SecObjectCommandTermList();

        protected override string GetCommandTerm()
        {
            return "on";
        }

        protected internal override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return _secObjectCommandTermList;
        }
    }
}