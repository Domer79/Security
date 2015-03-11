using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
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
            MapToStoredProcedures(SetInsert);
            MapToStoredProcedures(SetUpdate);
            MapToStoredProcedures(SetDelete);
        }

        private void SetDelete(ModificationStoredProceduresConfiguration<UserGroupsDetail> p)
        {
            p.Delete(d => d.HasName("sec.DeleteUserFromGroup"));
        }

        private void SetUpdate(ModificationStoredProceduresConfiguration<UserGroupsDetail> p)
        {
            p.Update(u => u.HasName("sec.UpdateUserGroup"));
        }

        private void SetInsert(ModificationStoredProceduresConfiguration<UserGroupsDetail> p)
        {
            p.Insert(i => i.HasName("sec.AddUserToGroup"));
        }
    }
}
