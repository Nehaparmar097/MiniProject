using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions.RepositoryException
{
    [Serializable]
    internal class BloodStockRepositoryException : Exception
    {
        public BloodStockRepositoryException()
        {
        }

        public BloodStockRepositoryException(string? message) : base(message)
        {
        }

        public BloodStockRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BloodStockRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}