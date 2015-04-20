using System.Collections.Generic;
using WebSecurity.IntellISense.Base;

namespace WebSecurity.IntellISense.Grant.AccessTypes.Base
{
    internal abstract class CommandTermAccessTypeBase : CommandTermBase
    {
        private const int OptionalDepth = 4;
        private readonly int _depth;

        private readonly CommandTermAccessTypeBase _commandTermSelect;
        private readonly CommandTermAccessTypeBase _commandTermInsert;
        private readonly CommandTermAccessTypeBase _commandTermUpdate;
        private readonly CommandTermAccessTypeBase _commandTermDelete;
        private readonly CommandTermTo _commandTermTo;

        protected CommandTermAccessTypeBase()
        {
        }

        internal CommandTermAccessTypeBase(int depth)
        {
            _depth = depth;
            _commandTermTo = new CommandTermTo();
            if (depth == OptionalDepth)
                return;

            _commandTermSelect = new CommandTermSelect(depth+1);
            _commandTermInsert = new CommandTermInsert(depth+1);
            _commandTermUpdate = new CommandTermUpdate(depth+1);
            _commandTermDelete = new CommandTermDelete(depth+1);
        }

        protected override int GetMaxOptionalDepth()
        {
            return OptionalDepth;
        }

        protected internal sealed override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            if (_depth < OptionalDepth)
            {
                yield return _commandTermSelect;
                yield return _commandTermInsert;
                yield return _commandTermUpdate;
                yield return _commandTermDelete;
            }
            yield return _commandTermTo;
        }
    }
}