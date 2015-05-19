using System.Linq;
using SystemTools.Interfaces;
using DataRepository;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class GroupRepository : SecurityDataModel.Repositories.GroupRepository
    {
        public GroupRepository()
        {
        }

        public GroupRepository(RepositoryDataContext context)
        {
            SetContext(context);
        }

        public static IQueryable<IGroup> GetGroupCollection()
        {
            return new GroupRepository().GetQueryableCollection();
        }
    }
}
