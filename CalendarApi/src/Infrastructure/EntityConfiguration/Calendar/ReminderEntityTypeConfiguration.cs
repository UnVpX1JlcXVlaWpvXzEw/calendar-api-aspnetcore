namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ReminderEntityTypeConfiguration : EntityTypeConfiguration<Domain.Aggregate.Calendar.Reminder>
    {
        protected override string TableName => "Reminders";

        protected override void ConfigureEntity(EntityTypeBuilder<Domain.Aggregate.Calendar.Reminder> builder)
        {
            builder.Property(r => r.OffsetInMinutes)
                .IsRequired();

            builder.Property(r => r.Method)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(r => r.Enabled)
                .IsRequired();
        }
    }
}
