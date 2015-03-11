using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class UserGroupsDetailRepositoryLocal : RepositoryBase<UserGroupsDetail>
    {
        private readonly SecurityContext _localContext;

        public UserGroupsDetailRepositoryLocal(SecurityContext context)
        {
            _localContext = context;
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