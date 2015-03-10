using System.Data.Entity.ModelConfiguration.Configuration;
using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    public class GroupConfiguration : BaseConfiguration<Group>
    {
        public GroupConfiguration()
        {
            Property(e => e.GroupName)
                .IsUnicode(false);

            Property(e => e.GroupDescription)
                .IsUnicode(false);

            MapToStoredProcedures(InsertConfiguration);
            MapToStoredProcedures(UpdateConfiguration);
            MapToStoredProcedures(DeleteConfiguration);
        }
        private void DeleteConfiguration(ModificationStoredProceduresConfiguration<Group> p)
        {
            p.Delete(d => d.HasName("sec.DeleteMember").Parameter(p0 => p0.IdGroup, "idMember"));
        }

        private static void UpdateConfiguration(ModificationStoredProceduresConfiguration<Group> p)
        {
            p.Update(u => u.HasName("sec.UpdateGroup"));
        }

        public void InsertConfiguration(ModificationStoredProceduresConfiguration<Group> p)
        {
            p.Insert(i => i.HasName("sec.AddGroup").Result(r => r.IdGroup, "idMember"));
        }
    }
}