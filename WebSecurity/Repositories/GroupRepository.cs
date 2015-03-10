using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.Models;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class GroupRepository : SecurityDataModel.Repositories.GroupRepository
    {
        public GroupRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
