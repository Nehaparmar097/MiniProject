using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions
{
    [Serializable]
    internal class DonorServiceException : Exception
    {
        public DonorServiceException()
        {
        }

        public DonorServiceException(string? message) : base(message)
        {
        }

        public DonorServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DonorServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}