using System.Data.Entity;
using DataModel.Models;
using DataRepository;

namespace DataModel.Repositories
{
    public class GrantRepository : RepositoryBase<SGrant>
    {
        protected override DbContext GetContext()
        {
            return new SecurityContext();
        }
    }
}
