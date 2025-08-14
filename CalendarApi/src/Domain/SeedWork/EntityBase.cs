namespace HustleAddiction.Platform.CalendarApi.Domain.SeedWork
{
    public abstract class EntityBase : ComparableSeed
    {
        protected EntityBase()
        {
            this.UUId = Guid.NewGuid();
        }

        public DateTime CreationDate { get; set; }

        public long Id { get; set; }

        public DateTime ModificationDate { get; set; }

        public Guid UUId { get; set; }

        public override bool Equals(ComparableSeed other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var isTransient = (other as EntityBase)?.IsTransient();

            return (isTransient is null || !isTransient.Value)
                && base.Equals(other);
        }

        public bool IsTransient() => this.Id == default(Int32);

        public void UpdateEntityBase(
            DateTime creationDate,
            DateTime modificationDate,
            Guid uuid, long id)
        {
            this.CreationDate = creationDate;
            this.ModificationDate = modificationDate;
            this.UUId = uuid;
            this.Id = id;
        }
    }
}