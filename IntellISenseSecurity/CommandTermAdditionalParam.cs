using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace IntellISenseSecurity
{
    public class CommandTermAdditionalParam : CommandTermBase
    {
        private readonly string _term;

        public CommandTermAdditionalParam(string term)
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
