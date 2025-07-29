namespace HustleAddiction.Platform.CalendarApi.Domain.EntityConfiguration
{
    using Domain.SeedWork;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : EntityBase
    {
        protected abstract string TableName { get; }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(this.TableName);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreationDate)
                .IsRequired();

            builder.Property(t => t.ModificationDate)
                .IsRequired()
                .IsConcurrencyToken();

            builder.Property(t => t.UUId)
                .IsRequired();

            this.ConfigureEntity(builder);

            builder.HasIndex(t => t.UUId)
                .IsUnique();
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
