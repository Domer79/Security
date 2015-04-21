using System.Collections.Generic;
using System.Linq;
using IntellISenseSecurity.Base;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense.Delete
{
    internal class CommandTermMember : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "member";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params)
        {
            var query = UserRepository.GetUserCollection().Select(u => u.Login).Union(GroupRepository.GetGroupCollection().Select(g => g.GroupName));
            return query.ToList().Select(u => new CommandTermMemberName { MemberName = u });
        }
    }
}