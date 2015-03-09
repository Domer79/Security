using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    internal class RoleConfiguration : BaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(e => e.RoleName)
                .IsUnicode(false);

            HasMany(e => e.Grants).WithRequired(e => e.Role);
            HasMany(e => e.RoleOfMembers).WithRequired(e => e.Role);
        }
    }
}