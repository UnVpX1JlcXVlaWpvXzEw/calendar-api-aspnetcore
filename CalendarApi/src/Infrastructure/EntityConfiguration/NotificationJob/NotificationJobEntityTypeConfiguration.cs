namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.NotificationJob
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class NotificationJobEntityTypeConfiguration : EntityTypeConfiguration<Domain.Aggregate.NotificationJob.NotificationJob>
    {
        protected override string TableName => "NotificationJob";

        protected override void ConfigureEntity(EntityTypeBuilder<Domain.Aggregate.NotificationJob.NotificationJob> builder)
        {
            builder.Property(x => x.TargetUserId)
                .IsRequired();

            builder.Property(x => x.EventId)
                .IsRequired();

            builder.Property(x => x.CalendarId)
                .IsRequired();

            builder.Property(x => x.ReminderOffset)
                .IsRequired();

            builder.Property(x => x.ScheduledTime)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.Channel)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}
