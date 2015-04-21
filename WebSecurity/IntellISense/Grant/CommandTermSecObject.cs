using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermSecObject : CommandTermBase
    {
        private readonly string _objectName;

        public CommandTermSecObject(string objectName)
        {
            _objectName = objectName;
        }

        protected override string GetCommandTerm()
        {
            return _objectName;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return null;
        }
    }
}