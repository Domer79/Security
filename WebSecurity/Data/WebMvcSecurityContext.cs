using System.Data.Entity;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class WebMvcSecurityContext : SecurityContext
    {
        public DbSet<TableObject> TableObjects { get; set; }
        public DbSet<ActionResultObject> ActionResultObjects { get; set; }
    }
}
