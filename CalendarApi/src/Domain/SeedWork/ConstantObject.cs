namespace HustleAddiction.Platform.CalendarApi.Domain.SeedWork
{
    public abstract class ConstantObject
    {
        protected ConstantObject(string value)
        {
            this.Value = value;
        }

        public string Value { get; init; }
    }
}
