using SecurityDataModel.Models;

namespace SecurityDataModel.EntityConfigurations
{
    public class AccessTypeConfiguration : BaseConfiguration<AccessType>
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.Object"/>.
        /// </summary>
        public AccessTypeConfiguration()
        {
            Property(e => e.AccessName)
                .IsUnicode(false);

            HasMany(e => e.Grants).WithRequired(e => e.AccessType);
        }
    }
}