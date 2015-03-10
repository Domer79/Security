using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    public class MemberConfiguration : BaseConfiguration<Member>
    {
        public MemberConfiguration()
        {
            Property(e => e.MemberName)
                .IsUnicode(false);
        }
    }
}