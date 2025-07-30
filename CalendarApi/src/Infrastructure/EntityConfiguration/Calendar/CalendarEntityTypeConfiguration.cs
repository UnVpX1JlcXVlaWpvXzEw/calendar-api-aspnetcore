using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CalendarEntity = HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Calendar;

namespace HustleAddiction.Platform.CalendarApi.Domain.EntityConfiguration.Calendar
{
    internal class CalendarEntityTypeConfiguration : EntityTypeConfiguration<CalendarEntity>
    {
        protected override string TableName => "Calendars";

        protected override void ConfigureEntity(EntityTypeBuilder<CalendarEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.Events)
                .WithOne()
                .HasForeignKey(e => e.OwnerId);
        }
    }
}
