using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CalendarEntity = HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar.Calendar;


namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar
{
    internal class EventEntityTypeConfiguration : EntityTypeConfiguration<Event>
    {
        protected override string TableName => "Events";

        protected override void ConfigureEntity(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.StartTime)
                .IsRequired();

            builder.Property(e => e.EndTime)
                .IsRequired();

            builder.Property<Guid>("CalendarId")
                   .IsRequired();

            builder.HasOne<CalendarEntity>()
                   .WithMany(c => c.Events)
                   .HasForeignKey("CalendarId")
                   .HasPrincipalKey(c => c.UUId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
