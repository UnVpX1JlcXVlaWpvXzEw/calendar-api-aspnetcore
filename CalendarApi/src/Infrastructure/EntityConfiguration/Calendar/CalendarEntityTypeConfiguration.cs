using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CalendarEntity = HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Calendar;


namespace HustleAddiction.Platform.CalendarApi.Domain.EntityConfiguration.Calendar
{
    public class CalendarEntityTypeConfiguration : IEntityTypeConfiguration<CalendarEntity>
    {
        public void Configure(EntityTypeBuilder<CalendarEntity> builder)
        {
            builder.ToTable("Calendars");

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