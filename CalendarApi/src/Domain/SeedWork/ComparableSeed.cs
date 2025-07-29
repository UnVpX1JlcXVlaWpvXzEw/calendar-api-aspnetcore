namespace HustleAddiction.Platform.CalendarApi.Domain.SeedWork
{
    public abstract class ComparableSeed : IEquatable<ComparableSeed>
    {
        public static bool operator !=(
            ComparableSeed leftHand,
            ComparableSeed rightHand) => !(
            leftHand == rightHand);

        public static bool operator ==(ComparableSeed leftHand, ComparableSeed rightHand)
        {
            if (leftHand is null)
            {
                if (rightHand is null)
                {
                    return true;
                }

                return false;
            }

            return leftHand.Equals(rightHand);
        }

        public override bool Equals(object obj) => Equals(obj as ComparableSeed);

        public virtual bool Equals(ComparableSeed other)
        {
            IEnumerator<object> thisValues = this.GetAtomicValues()
                .GetEnumerator();

            IEnumerator<object> otherValues = other.GetAtomicValues()
                .GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null && !thisValues.Current
                    .Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return this.GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        protected abstract IEnumerable<object> GetAtomicValues();
    }
}
