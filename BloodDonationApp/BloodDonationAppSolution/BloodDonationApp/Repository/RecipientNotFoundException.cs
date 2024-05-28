using System.Runtime.Serialization;

namespace BloodDonationApp.Repository
{
    [Serializable]
    internal class RecipientNotFoundException : Exception
    {
        public RecipientNotFoundException()
        {
        }

        public RecipientNotFoundException(string? message) : base(message)
        {
        }

        public RecipientNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecipientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}