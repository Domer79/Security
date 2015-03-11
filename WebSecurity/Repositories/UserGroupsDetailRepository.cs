using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class UserGroupsDetailRepository : SecurityDataModel.Repositories.UserGroupsDetailRepository
    {
        public UserGroupsDetailRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
