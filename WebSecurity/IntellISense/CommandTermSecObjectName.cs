using System.Collections.Generic;
using IntellISenseSecurity.Base;

namespace WebSecurity.IntellISense
{
    internal class CommandTermSecObjectName : CommandTermBase
    {
        private readonly string _objectName;

        public CommandTermSecObjectName(string objectName)
        {
            _objectName = objectName;
        }

        protected override string GetCommandTerm()
        {
            return _objectName;
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            return NextCommandTermList;
        }

        public IEnumerable<CommandTermBase> NextCommandTermList { get; set; }
    }
}