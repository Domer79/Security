using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.Common;

namespace WebSecurity.IntellISense.Add
{
    public class CommandTermAdd : CommandTermBase
    {
        private readonly CommandTermCommonUser _commandTermCommonUser = new CommandTermCommonUser();
        private readonly CommandTermCommonGroup _commandTermCommonGroup = new CommandTermCommonGroup();
        private readonly CommandTermCommonRole _commandTermCommonRole = new CommandTermCommonRole();
        private readonly CommandTermCommonController _commandTermController = new CommandTermCommonController();
        private readonly CommandTermCommonTable _commandTermTable = new CommandTermCommonTable();
        private readonly CommandTermBase _commandTermAllSecurityObjects = new CommandTermAllSecurityObjects();

        public CommandTermAdd()
        {
            NextCommandTerms = new List<CommandTermBase>(GetNextCommandTerms());
        }

        protected override string GetCommandTerm()
        {
            return "add";
        }

        private IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermCommonUser;
            yield return _commandTermCommonGroup;
            yield return _commandTermCommonRole;
            yield return _commandTermController;
            yield return _commandTermTable;
            yield return _commandTermAllSecurityObjects;
        }
    }
}
