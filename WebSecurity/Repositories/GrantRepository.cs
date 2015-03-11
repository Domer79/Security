using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class GrantRepository : SecurityDataModel.Repositories.GrantRepository
    {
        public GrantRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
