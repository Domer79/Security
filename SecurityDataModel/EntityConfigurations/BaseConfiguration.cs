using System.Data.Entity.ModelConfiguration;

namespace SecurityDataModel.EntityConfigurations
{
    public class BaseConfiguration<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public const string Schema = "sec";
    }
}