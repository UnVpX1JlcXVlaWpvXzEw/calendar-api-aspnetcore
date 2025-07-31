using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace HustleAddiction.Platform.CalendarApi.Domain.Exceptions
{
    [Serializable]
    public class CalendarAPIException : Exception
    {
        [NonSerialized]
        private readonly int errorCode;

        public CalendarAPIException(int errorCode)
            : base()
        {
            this.errorCode = errorCode;
        }

        public CalendarAPIException(string message, int errorCode)
            : base(message)
        {
            this.errorCode = errorCode;
        }

        public CalendarAPIException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CalendarAPIException(
            SerializationInfo info,
            StreamingContext context,
            int errorCode)
            : this(info, context)
        {
            this.errorCode = errorCode;
        }

        protected CalendarAPIException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public int ErrorCode => this.errorCode;
    }
}