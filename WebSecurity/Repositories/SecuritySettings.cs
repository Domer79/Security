using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using SecurityDataModel.Models;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class SecuritySettings : SecurityDataModel.Repositories.SecuritySettings
    {
        public SecuritySettings() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
