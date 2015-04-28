using System.Collections.Generic;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;

namespace WebSecurity.IntellISense.Grant.AccessTypes.Base
{
    internal abstract class CommandTermAccessTypeBase : CommandTermBase
    {
        private const int OptionalDepth = 4;
        private readonly int _depth;

        protected CommandTermAccessTypeBase()
        {
//            NextCommandTerms = new List<CommandTermBase>(GetNextCommandTerms());
        }

        internal CommandTermAccessTypeBase(int depth)
        {
            _depth = depth;

            NextCommandTerms = new List<CommandTermBase>(GetNextCommandTerms());
        }

        protected override int GetMaxOptionalDepth()
        {
            return OptionalDepth;
        }

        private IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            if (_depth < OptionalDepth)
            {
                yield return new CommandTermSelect(_depth + 1);
                yield return new CommandTermInsert(_depth + 1);
                yield return new CommandTermUpdate(_depth + 1);
                yield return new CommandTermDelete(_depth + 1);
            }
            yield return new CommandTermTo();
        }
    }
}