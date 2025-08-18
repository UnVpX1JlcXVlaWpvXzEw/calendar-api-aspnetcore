namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.NotificationJob
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class NotificationJobEntityTipeConfiguration : EntityTypeConfiguration<Domain.Aggregate.NotificationJob.NotificationJob>
    {
        protected override string TableName => "NotificationJob";

        protected override void ConfigureEntity(EntityTypeBuilder<Domain.Aggregate.NotificationJob.NotificationJob> builder)
        {

        }
    }
}
