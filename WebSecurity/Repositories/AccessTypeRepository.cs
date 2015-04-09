using System;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class AccessTypeRepository : SecurityDataModel.Repositories.AccessTypeRepository
    {
        public AccessTypeRepository() 
            : base(new WebMvcSecurityContext())
        {
        }

        public static IAccessType GetExecAccessType()
        {
            return new AccessTypeRepository().GetSingleAccessType(SecurityAccessType.Exec);
        }

        public static IAccessType[] GetAccessTypes(SecurityAccessType accessType)
        {
            return ((SecurityDataModel.Repositories.AccessTypeRepository) new AccessTypeRepository()).GetAccessTypes(accessType);
        }
    }
}
