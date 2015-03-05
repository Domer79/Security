using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxorgRepository.Models;

namespace TaxorgRepository.Repositories
{
    public class Repository<TEntity> : RepositoryBase<TEntity> 
        where TEntity : ModelBase
    {
    }
}
