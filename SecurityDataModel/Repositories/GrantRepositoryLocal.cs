using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class GrantRepositoryLocal : RepositoryBase<Grant>
    {
        private readonly SecurityContext _localContext;

        public GrantRepositoryLocal()
        {
            _localContext = Tools.Context;
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