using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class RoleOfMemberRepositoryLocal : RepositoryBase<RoleOfMember>
    {
        private readonly SecurityContext _localContext;

        public RoleOfMemberRepositoryLocal(SecurityContext context)
        {
            _localContext = context;
        }

        protected override DbContext GetContext()
        {
            return _localContext;
        }

        public sealed override void InsertOrUpdate(RoleOfMember item)
        {
            Set.Add(item);
        }
    }
}