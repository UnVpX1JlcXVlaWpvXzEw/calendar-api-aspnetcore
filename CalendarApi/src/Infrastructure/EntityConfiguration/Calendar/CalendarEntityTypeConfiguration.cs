using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar
{
    internal class CalendarEntityTypeConfiguration : EntityTypeConfiguration<Domain.Aggregate.Calendar.Calendar>
    {
        protected override string TableName => "Calendars";

        protected override void ConfigureEntity(EntityTypeBuilder<Domain.Aggregate.Calendar.Calendar> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.OwnerId)
                .IsRequired();

            builder.HasMany(c => c.Events)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
