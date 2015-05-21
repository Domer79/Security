using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    internal class GrantRepositoryLocal : SecurityRepository<Grant>
    {
        public override void InsertOrUpdate(Grant item)
        {
            Set.Add(item);
        }
    }
}