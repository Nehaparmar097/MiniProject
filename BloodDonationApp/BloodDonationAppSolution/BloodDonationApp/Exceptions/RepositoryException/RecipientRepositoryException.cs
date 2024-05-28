using System.Runtime.Serialization;

namespace BloodDonationApp.Exceptions.RepositoryException
{
    [Serializable]
    internal class RecipientRepositoryException : Exception
    {
        public RecipientRepositoryException()
        {
        }

        public RecipientRepositoryException(string? message) : base(message)
        {
        }

        public RecipientRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RecipientRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}