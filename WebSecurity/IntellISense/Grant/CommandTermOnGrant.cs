using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermOnGrant : CommandTermBase
    {
        public IQueryable<ISecObject> SecObjectCollection { get; set; }

        protected override string GetCommandTerm()
        {
            return "on";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return SecObjectCollection.ToList().Select(so => new CommandTermSecObject(so.ObjectName));
        }
    }
}