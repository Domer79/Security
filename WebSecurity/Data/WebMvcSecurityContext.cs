using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class WebMvcSecurityContext : SecurityContext
    {
        public DbSet<TableObject> TableObjects { get; set; }
        public DbSet<ActionResultObject> ActionResultObjects { get; set; }
    }
}
