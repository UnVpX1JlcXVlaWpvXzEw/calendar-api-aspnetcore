namespace HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class RecurrenceRuleEntityTypeConfiguration : EntityTypeConfiguration<RecurrenceRule>
    {
        protected override string TableName => "RecurrenceRules";

        protected override void ConfigureEntity(EntityTypeBuilder<RecurrenceRule> builder)
        {
            builder.Property(r => r.Frequency)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r => r.Start)
                .IsRequired();

            builder.Property(r => r.Count)
                .IsRequired(false);

            builder.Property(r => r.Until)
                .IsRequired(false);
        }
    }
}
