using System.Data.Entity.ModelConfiguration.Configuration;
using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(e => e.Login)
                .IsUnicode(false);

            Property(e => e.DisplayName)
                .IsUnicode(false);

            Property(e => e.Email)
                .IsUnicode(false);

            Property(e => e.Usersid)
                .IsUnicode(false);

//            Property(e => e.login).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAttribute())

            MapToStoredProcedures(InsertConfiguration);
            MapToStoredProcedures(UpdateConfiguration);
            MapToStoredProcedures(DeleteConfiguration);
        }

        private void DeleteConfiguration(ModificationStoredProceduresConfiguration<User> p)
        {
            p.Delete(d => d.HasName("sec.DeleteMember").Parameter(p0 => p0.IdUser, "idMember"));
        }

        private static void UpdateConfiguration(ModificationStoredProceduresConfiguration<User> p)
        {
            p.Update(u => u.HasName("sec.UpdateUser"));
        }

        public void InsertConfiguration(ModificationStoredProceduresConfiguration<User> p)
        {
            p.Insert(i => i.HasName("sec.AddUser").Result(r => r.IdUser, "idMember"));
        }
    }
}