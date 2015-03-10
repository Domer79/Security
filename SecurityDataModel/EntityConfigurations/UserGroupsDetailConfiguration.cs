using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    internal class UserGroupsDetailConfiguration : BaseConfiguration<UserGroupsDetail>
    {
        public UserGroupsDetailConfiguration()
        {
            MapToStoredProcedures();
        }
    }
}
