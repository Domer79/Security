using System.Collections.Generic;

namespace WebSecurity.IntellISense.Grant
{
    internal abstract class CommandTermAccessTypeBase : CommandTermBase
    {
        private const int OptionalDepth = 4;

//        private readonly CommandTermAccessTypeBase _commandTermExec;
        private readonly CommandTermAccessTypeBase _commandTermSelect;
        private readonly CommandTermAccessTypeBase _commandTermInsert;
        private readonly CommandTermAccessTypeBase _commandTermUpdate;
        private readonly CommandTermAccessTypeBase _commandTermDelete;

        protected CommandTermAccessTypeBase()
        {
        }

        internal CommandTermAccessTypeBase(int depth)
        {
            if (depth == OptionalDepth)
                return;

//            _commandTermExec = new CommandTermExec(depth+1);
            _commandTermSelect = new CommandTermSelect(depth+1);
            _commandTermInsert = new CommandTermInsert(depth+1);
            _commandTermUpdate = new CommandTermUpdate(depth+1);
            _commandTermDelete = new CommandTermDelete(depth+1);
        }

        protected override int GetMaxOptionalDepth()
        {
            return OptionalDepth;
        }

        protected sealed override IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
//            yield return _commandTermExec;
            yield return _commandTermSelect;
            yield return _commandTermInsert;
            yield return _commandTermUpdate;
            yield return _commandTermDelete;
        }
    }
}