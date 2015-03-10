using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    public class SecurityRepository<TEntity> : Repository<TEntity> where TEntity : ModelBase
    {
        public SecurityRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
