using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions
{
    [Serializable]
    internal class RecipientServiceException : Exception
    {
        public RecipientServiceException()
        {
        }

        public RecipientServiceException(string? message) : base(message)
        {
        }

        public RecipientServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecipientServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}