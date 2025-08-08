namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class RecurrenceExceptionEntityTypeConfiguration : EntityTypeConfiguration<RecurrenceException>
    {
        protected override string TableName => "RecurrenceExceptions";

        protected override void ConfigureEntity(EntityTypeBuilder<RecurrenceException> builder)
        {
            builder.Property(e => e.OriginalDate)
                .IsRequired();

            builder.Property(e => e.OverrideTitle)
                .HasMaxLength(200);

            builder.Property(e => e.OverrideTime);

            builder.Property(e => e.OverrideLocation)
                .HasMaxLength(200);
        }
    }
}
