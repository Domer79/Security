using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class GrantRepository
    {
        private readonly Repository<Grant> _repo;

        public GrantRepository(SecurityContext context)
        {
            _repo = new Repository<Grant>(context);
        }

        public void AddGrant(int idSecObject, int idRole, int idAccessType)
        {
            
        }
    }
}
