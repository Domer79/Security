using System.Data.Entity.ModelConfiguration.Configuration;
using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    internal class RoleOfMemberConfiguration : BaseConfiguration<RoleOfMember>
    {
        public RoleOfMemberConfiguration()
        {
            Property(e => e.RoleName)
                .IsUnicode(false);

            Property(e => e.MemberName)
                .IsUnicode(false);

            MapToStoredProcedures(SetInsert);
            MapToStoredProcedures(SetUpdate);
            MapToStoredProcedures(SetDelete);
        }

        private void SetDelete(ModificationStoredProceduresConfiguration<RoleOfMember> p)
        {
            p.Delete(d => d.HasName("sec.DeleteMemberRole")
                .Parameter(p0 => p0.IdMember, "idMember")
                .Parameter(p1 => p1.IdRole, "idRole"));
        }

        private static void SetUpdate(ModificationStoredProceduresConfiguration<RoleOfMember> p)
        {
            p.Update(u => u.HasName("sec.UpdateMemberRole")
                .Parameter(p0 => p0.IdMember, "idMember")
                .Parameter(p1 => p1.IdRole, "idRole"));
        }

        private static void SetInsert(ModificationStoredProceduresConfiguration<RoleOfMember> p)
        {
            p.Insert(i => i.HasName("sec.AddMemberRole")
                .Parameter(p0 => p0.IdMember, "idMember")
                .Parameter(p1 => p1.IdRole, "idRole"));
        }
    }
}