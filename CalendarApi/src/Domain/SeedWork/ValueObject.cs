namespace HustleAddiction.Platform.CalendarApi.Domain.SeedWork
{
    public abstract class ValueObject : ComparableSeed
    {
        public override bool Equals(ComparableSeed other)
        {
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return base.Equals((ValueObject)other);
        }

        public ValueObject? GetCopy()
        {
            return this.MemberwiseClone() as ValueObject;
        }
    }
}