using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class RoleOfMemberRepositoryLocal : RepositoryBase<RoleOfMember>
    {
        private readonly SecurityContext _localContext;

        public RoleOfMemberRepositoryLocal()
        {
            _localContext = Tools.Context;
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