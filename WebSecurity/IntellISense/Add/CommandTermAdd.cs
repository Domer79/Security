using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity.Base;
using WebSecurity.IntellISense.CommandTermCommon;

namespace WebSecurity.IntellISense.Add
{
    public class CommandTermAdd : CommandTermBase
    {
        private readonly CommandTermCommonUser _commandTermCommonUser = new CommandTermCommonUser();
        private readonly CommandTermCommonGroup _commandTermCommonGroup = new CommandTermCommonGroup();
        private readonly CommandTermCommonRole _commandTermCommonRole = new CommandTermCommonRole();
        private readonly CommandTermController _commandTermController = new CommandTermController();
        private readonly CommandTermTable _commandTermTable = new CommandTermTable();

        protected override string GetCommandTerm()
        {
            return "add";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            yield return _commandTermCommonUser;
            yield return _commandTermCommonGroup;
            yield return _commandTermCommonRole;
            yield return _commandTermController;
            yield return _commandTermTable;
        }
    }
}
