using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class UserGroupsDetailRepositoryLocal : SecurityRepository<UserGroupsDetail>
    {
        public override void InsertOrUpdate(UserGroupsDetail item)
        {
            Set.Add(item);
        }
    }
}