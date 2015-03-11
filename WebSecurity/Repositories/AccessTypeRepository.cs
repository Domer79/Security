using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class AccessTypeRepository : SecurityDataModel.Repositories.AccessTypeRepository
    {
        public AccessTypeRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
