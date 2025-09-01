namespace HustleAddiction.Platform.CalendarApi.Domain.Aggregate.NotificationJob
{
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Enums;
    using HustleAddiction.Platform.CalendarApi.Domain.SeedWork;
    using System;
    using System.Collections.Generic;

    public class NotificationJob : EntityBase, IAggregateRoot
    {
        public Guid TargetUserId { get; set; }

        public Guid EventId { get; set; }

        public Guid CalendarId { get; set; }

        public int ReminderOffset { get; set; }

        public DateTime ScheduledTime { get; set; }

        public Status Status { get; set; }

        public Channel Channel { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.UUId;
        }

        public void CalculateScheduledTime(DateTime eventStartTime)
        {
            ScheduledTime = eventStartTime.AddMinutes(ReminderOffset);
        }

        public void ValidateOffset()
        {
            if (ReminderOffset > 0)
                throw new ArgumentException("ReminderOffset must be less than or equal to 0.");
        }

        public void Send()
        {
            Status = Status.SENT;
        }
    }
}
