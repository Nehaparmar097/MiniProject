using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions.RepositoryException
{
    [Serializable]
    internal class BloodDonationRepositoryException : Exception
    {
        public BloodDonationRepositoryException()
        {
        }

        public BloodDonationRepositoryException(string? message) : base(message)
        {
        }

        public BloodDonationRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BloodDonationRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}