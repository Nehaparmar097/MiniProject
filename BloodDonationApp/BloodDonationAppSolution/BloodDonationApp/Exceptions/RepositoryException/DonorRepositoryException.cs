using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions.RepositoryException
{
    [Serializable]
    internal class DonorRepositoryException : Exception
    {
        public DonorRepositoryException()
        {
        }

        public DonorRepositoryException(string? message) : base(message)
        {
        }

        public DonorRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DonorRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}