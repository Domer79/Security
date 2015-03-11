using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    public class GrantConfiguration : BaseConfiguration<Grant>
    {
        public GrantConfiguration()
        {
            Property(e => e.ObjectName)
                .IsUnicode(false);

            Property(e => e.Type1)
                .IsUnicode(false);

            Property(e => e.Type2)
                .IsUnicode(false);

            Property(e => e.Type3)
                .IsUnicode(false);

            Property(e => e.Type4)
                .IsUnicode(false);

            Property(e => e.Type5)
                .IsUnicode(false);

            Property(e => e.Type6)
                .IsUnicode(false);

            Property(e => e.Type7)
                .IsUnicode(false);

            Property(e => e.RoleName)
                .IsUnicode(false);

            Property(e => e.AccessName)
                .IsUnicode(false);

            MapToStoredProcedures(p => p.Insert(i => i.HasName("sec.AddGrant")));
            MapToStoredProcedures(p => p.Update(u => u.HasName("sec.UpdateGrant")));
            MapToStoredProcedures(p => p.Delete(d => d.HasName("sec.DeleteGrant")));
        }
    }
}