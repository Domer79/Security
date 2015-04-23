using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class RoleOfMemberRepositoryLocal : SecurityRepository<RoleOfMember>
    {
        public sealed override void InsertOrUpdate(RoleOfMember item)
        {
            Set.Add(item);
        }
    }
}