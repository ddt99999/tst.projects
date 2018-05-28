using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhoenixSea.Common.Data.EF
{
    public class BaseEntityMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public BaseEntityMapping()
        {
            Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}