using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense
{
    public class CommandTerm : CommandTermBase
    {
        private readonly string _term;

        public CommandTerm(string term)
        {
            _term = term;
        }

        protected override string GetCommandTerm()
        {
            return _term;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return null;
        }
    }
}
