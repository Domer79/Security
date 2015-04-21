using System.Linq;
using SystemTools.Interfaces;

namespace WebSecurity.Repositories
{
    internal class GroupRepository : SecurityDataModel.Repositories.GroupRepository
    {
        public static IQueryable<IGroup> GetGroupCollection()
        {
            return new GroupRepository().GetQueryableCollection();
        }
    }
}
