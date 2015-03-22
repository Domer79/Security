using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class AccessTypeRepository : SecurityDataModel.Repositories.AccessTypeRepository
    {
        public AccessTypeRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
