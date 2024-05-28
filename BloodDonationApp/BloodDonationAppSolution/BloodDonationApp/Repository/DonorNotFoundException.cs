using System.Runtime.Serialization;

namespace BloodDonationApp.Repository
{
    [Serializable]
    internal class DonorNotFoundException : Exception
    {
        public DonorNotFoundException()
        {
        }

        public DonorNotFoundException(string? message) : base(message)
        {
        }

        public DonorNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DonorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}