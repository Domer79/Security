using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class UserGroupsDetailRepositoryLocal : RepositoryBase<UserGroupsDetail>
    {
        private readonly SecurityContext _localContext;

        public UserGroupsDetailRepositoryLocal()
        {
            _localContext = Tools.Context;
        }

        protected override DbContext GetContext()
        {
            return _localContext;
        }

        public override void InsertOrUpdate(UserGroupsDetail item)
        {
            Set.Add(item);
        }
    }
}