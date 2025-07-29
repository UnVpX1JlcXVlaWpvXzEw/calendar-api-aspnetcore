using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.UUId);
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.StartTime)
                .IsRequired();

            builder.Property(e => e.EndTime)
                .IsRequired();
        }
    }
}
