using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class GrantRepositoryLocal : RepositoryBase<Grant>
    {
        private readonly SecurityContext _localContext;

        public GrantRepositoryLocal(SecurityContext context)
        {
            _localContext = context;
        }

        protected override DbContext GetContext()
        {
            return _localContext;
        }

        public override void InsertOrUpdate(Grant item)
        {
            Set.Add(item);
        }
    }
}