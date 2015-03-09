using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    public class SecObjectConfiguration : BaseConfiguration<SecObject>
    {
        public SecObjectConfiguration()
        {
//            Property(e => e.ObjectName)
//                .IsUnicode(false);
//
//            Property(e => e.type1)
//                .IsUnicode(false);
//
//            Property(e => e.type2)
//                .IsUnicode(false);
//
//            Property(e => e.type3)
//                .IsUnicode(false);
//
//            Property(e => e.type4)
//                .IsUnicode(false);
//
//            Property(e => e.type5)
//                .IsUnicode(false);
//
//            Property(e => e.type6)
//                .IsUnicode(false);
//
//            Property(e => e.type7)
//                .IsUnicode(false);

            HasMany(e => e.Grants).WithRequired(e => e.SecObject);
        }
    }
}