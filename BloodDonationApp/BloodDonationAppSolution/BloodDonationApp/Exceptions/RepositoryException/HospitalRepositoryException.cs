using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions.RepositoryException
{
    [Serializable]
    internal class HospitalRepositoryException : Exception
    {
        public HospitalRepositoryException()
        {
        }

        public HospitalRepositoryException(string? message) : base(message)
        {
        }

        public HospitalRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HospitalRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}